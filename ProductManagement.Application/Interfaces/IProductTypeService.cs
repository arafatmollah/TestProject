using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Interfaces
{
    public interface IProductTypeService
    {
        Task<List<ProductTypeDto>> GetAllAsync();
        
        Task<ProductTypeDto> CreateAsync(ProductTypeDto productTypeDto);
        Task<bool> UpdateAsync(Guid id, ProductTypeDto product);
        Task<bool> DeleteAsync(Guid id);
    }
}