using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync(
    string? search, string? productType, decimal?price,
    CancellationToken cancellationToken = default);

    Task<ProductDto?> GetByIdAsync(Guid id);

    Task<ProductDto> CreateAsync(
    ProductDto dto,
    CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(Guid id, ProductDto product);

    Task<bool> DeleteAsync(Guid id);
}