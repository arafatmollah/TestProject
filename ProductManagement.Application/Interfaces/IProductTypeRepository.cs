using ProductTypeEntity = ProductManagement.Domain.Entities.ProductType;

namespace ProductManagement.Application.Interfaces;

public interface IProductTypeRepository
{
    Task<List<ProductTypeEntity>> GetAllAsync();

    Task<ProductTypeEntity?> GetByIdAsync(Guid id);

    Task<ProductTypeEntity> CreateAsync(
        ProductTypeEntity productType);

    Task<bool> UpdateAsync(
        Guid id,
        ProductTypeEntity productType);

    Task<bool> DeleteAsync(Guid id);
}