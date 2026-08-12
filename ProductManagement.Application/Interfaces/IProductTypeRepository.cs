using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Interfaces
{
    public interface IProductTypeRepository
    {
        Task<List<ProductType>> GetAllAsync();
        Task<ProductType?> GetByIdAsync(Guid id);
        Task<ProductType> CreateAsync(ProductType productType);
        Task<bool> UpdateAsync(Guid id, ProductType productType);
        Task<bool> DeleteAsync(Guid id);
    }
}