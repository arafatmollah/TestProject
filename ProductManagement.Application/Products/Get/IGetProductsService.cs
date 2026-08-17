using ProductManagement.Application.Common.Pagination;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Products.Get;

public interface IGetProductsService
{
    Task<PagedResult<ProductDto>> GetAllAsync(
         ProductFilter filter,
        CancellationToken cancellationToken = default);
}