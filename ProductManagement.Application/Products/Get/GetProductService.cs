using Microsoft.Extensions.Caching.Memory;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Application.Products.Get;

public class GetProductsService : IGetProductsService
{
    private readonly IProductRepository _repository;
    private readonly IMemoryCache _cache;

    public GetProductsService(
        IProductRepository repository,
        IMemoryCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<List<ProductDto>> GetAllAsync(
        string? search,
        string? productType,
        decimal? price,
        CancellationToken cancellationToken = default)
    {
        var cacheKey =
            $"products:{search}:{productType}:{price}";

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
            ProductTypeName =
                p.ProductType?.Name ?? string.Empty,
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
}