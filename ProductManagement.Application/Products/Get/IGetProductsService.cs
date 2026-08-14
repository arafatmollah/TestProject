using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Products.Get;

public interface IGetProductsService
{
    Task<List<ProductDto>> GetAllAsync(
        string? search,
        string? productType,
        decimal? price,
        CancellationToken cancellationToken = default);
}