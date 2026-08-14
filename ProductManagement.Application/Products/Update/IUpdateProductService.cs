using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Products.Update;

public interface IUpdateProductService
{
    Task<bool> UpdateAsync(
        Guid id,
        ProductDto dto,
        CancellationToken cancellationToken = default);
}