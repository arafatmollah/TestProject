using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync(string? search);

    Task<ProductDto?> GetByIdAsync(Guid id);

    Task<ProductDto> CreateAsync(ProductDto product);

    Task<bool> UpdateAsync(Guid id, ProductDto product);

    Task<bool> DeleteAsync(Guid id);
}