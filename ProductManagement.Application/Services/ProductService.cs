using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
namespace ProductManagement.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMemoryCache _cache;

    public ProductService(IProductRepository repository, IMemoryCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<List<ProductDto>> GetAllAsync(
    string? search,
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
            price,
            cancellationToken);

        var result = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price
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
            Price = product.Price
        };
    }

    public async Task<ProductDto> CreateAsync(ProductDto dto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(product);

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