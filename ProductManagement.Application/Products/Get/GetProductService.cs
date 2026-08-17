using Microsoft.Extensions.Caching.Memory;
using ProductManagement.Application.Common.Pagination;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using AutoMapper;
namespace ProductManagement.Application.Products.Get;

public class GetProductsService : IGetProductsService
{
    private readonly IProductRepository _repository;
    private readonly IMemoryCache _cache;
    private readonly IMapper _mapper;
    public GetProductsService(
        IProductRepository repository,
        IMemoryCache cache,
        IMapper mapper)
    {
        _repository = repository;
        _cache = cache;
        _mapper = mapper;
    }

    public async Task<PagedResult<ProductDto>> GetAllAsync(
    ProductFilter filter,
    CancellationToken cancellationToken = default)
    {
        var cacheKey =
            $"products:" +
            $"{filter.Search}:" +
            $"{filter.ProductType}:" +
            $"{filter.MinPrice}:" +
            $"{filter.MaxPrice}:" +
            $"{filter.Page}:" +
            $"{filter.PageSize}";

        if (_cache.TryGetValue(
            cacheKey,
            out PagedResult<ProductDto>? cachedProducts))
        {
            return cachedProducts!;
        }

        var products = await _repository.GetAllAsync(
            filter,
            cancellationToken);

        var items = _mapper.Map<List<ProductDto>>(
            products.Items);

        var result = new PagedResult<ProductDto>
        {
            Items = items,
            Page = products.Page,
            PageSize = products.PageSize,
            TotalCount = products.TotalCount,
            TotalPages = products.TotalPages
        };

        _cache.Set(
            cacheKey,
            result,
            TimeSpan.FromMinutes(5));

        return result;
    }
}