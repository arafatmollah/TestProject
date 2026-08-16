using ProductManagement.Application.Common.Pagination;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Products.Get;

public interface IGetProductsService
{
    Task<PagedResult<ProductDto>> GetAllAsync(
        string? search,
        string? productType,
        decimal? minPrice, decimal? maxPrice, int page,
        int pageSize,
        CancellationToken cancellationToken = default);
}