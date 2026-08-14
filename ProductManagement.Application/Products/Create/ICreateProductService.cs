using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Products.Create;

public interface ICreateProductService
{
    Task<ProductDto> CreateAsync(
        CreateProductDto dto,
        CancellationToken cancellationToken = default);
}