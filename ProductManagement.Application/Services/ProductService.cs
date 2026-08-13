using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using ProductManagement.Domain.Services;
namespace ProductManagement.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMemoryCache _cache;
    private readonly ProductDomainService _productDomainService;
    public ProductService(IProductRepository repository, IMemoryCache cache, ProductDomainService productDomainService)
    {
        _repository = repository;
        _cache = cache;
        _productDomainService = productDomainService;
    }

    public async Task<List<ProductDto>> GetAllAsync(
    string? search,
    string? productType,
    decimal? price,
    CancellationToken cancellationToken = default)
    {
        var cacheKey = $"products:{search}:{price}";

        if (_cache.TryGetValue(
            cacheKey,
            out List<ProductDto>? cachedProducts))
        {
            return cachedProducts!;
        }

        var products = await _repository.GetAllAsync(
            search,
            productType,
            price,
            cancellationToken);

        var result = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Quantity = p.Quantity,

            ProductTypeId = p.ProductTypeId,

            ProductTypeName = p.ProductType?.Name ?? string.Empty,

            ExpirationDate =
        p.ProductExpiration?.ExpirationDate,

            Tags = p.ProductTags
        .Select(t => t.Name)
        .ToList()

        }).ToList();

        _cache.Set(
            cacheKey,
            result,
            TimeSpan.FromMinutes(5));

        return result;
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
            return null;

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.Quantity,
            ProductTypeName = product.ProductType?.Name ?? string.Empty,
        };
    }

    public async Task<ProductDto> CreateAsync(ProductDto dto, CancellationToken cancellationToken = default)
    {
        var product = _productDomainService.CreateProduct(
    dto.Name,
    dto.Description,
    dto.Price,
    dto.Quantity,
    dto.ProductTypeId);

        if (dto.ExpirationDate.HasValue)
        {
            product.ProductExpiration = new ProductExpiration
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                ExpirationDate = dto.ExpirationDate.Value
            };
        }

        foreach (var tag in dto.Tags)
        {
            product.ProductTags.Add(new ProductTag
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Name = tag
            });
        }

        await _repository.AddAsync(product, cancellationToken);

        dto.Id = product.Id;

        return dto;
    }

    public async Task<bool> UpdateAsync(
        Guid id,
        ProductDto dto)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
            return false;

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;

        await _repository.UpdateAsync(product);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
            return false;

        await _repository.DeleteAsync(product);

        return true;
    }

    
}