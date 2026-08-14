using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Products.GetById;

public interface IGetProductByIdService
{
    Task<ProductDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}