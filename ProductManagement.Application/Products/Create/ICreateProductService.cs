using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Products.Create;

public interface ICreateProductService
{
    Task<ProductDto> CreateAsync(
        ProductDto dto,
        CancellationToken cancellationToken = default);
}