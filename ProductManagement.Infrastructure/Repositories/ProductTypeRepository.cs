using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Data;

namespace ProductManagement.Infrastructure.Repositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly AppDbContext _context;

        public ProductTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductType>> GetAllAsync()
        {
            return await _context.ProductTypes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProductType?> GetByIdAsync(Guid id)
        {
            return await _context.ProductTypes
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProductType> CreateAsync(ProductType productType)
        {
            await _context.ProductTypes.AddAsync(productType);
            await _context.SaveChangesAsync();

            return productType;
        }

        public async Task<bool> UpdateAsync(Guid id, ProductType productType)
        {
            var existingProductType = await _context.ProductTypes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingProductType == null)
                return false;

            existingProductType.Name = productType.Name;
            //existingProductType.Description = productType.Description;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var productType = await _context.ProductTypes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (productType == null)
                return false;

            _context.ProductTypes.Remove(productType);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}