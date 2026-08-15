using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Products.Update;

public interface IUpdateProductService
{
    Task<bool> UpdateAsync(
    Guid id,
    UpdateProductDto dto,
    CancellationToken cancellationToken = default);
}