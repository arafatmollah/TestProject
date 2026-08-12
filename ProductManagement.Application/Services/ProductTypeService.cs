using Microsoft.Extensions.Caching.Memory;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IMemoryCache _cache;
        private readonly IProductTypeRepository _repository;

        private const string CacheKey = "product_types";

        public ProductTypeService(
            IProductTypeRepository productTypeRepository,
            IMemoryCache cache)
        {
            _repository = productTypeRepository;
            _cache = cache;
        }

        public async Task<List<ProductTypeDto>> GetAllAsync()
        {
            if (_cache.TryGetValue(CacheKey, out List<ProductTypeDto>? cachedProducts))
            {
                return cachedProducts!;
            }

            var productTypes = await _repository.GetAllAsync();

            var result = productTypes.Select(p => new ProductTypeDto
            {
                Id = p.Id,
                Name = p.Name,
                //Description = p.Description
            }).ToList();

            _cache.Set(
                CacheKey,
                result,
                TimeSpan.FromMinutes(10));

            return result;
        }

        public async Task<ProductTypeDto?> GetByIdAsync(Guid id)
        {
            string cacheKey = $"product_type_{id}";

            if (_cache.TryGetValue(cacheKey, out ProductTypeDto? cachedProductType))
            {
                return cachedProductType;
            }

            var productType = await _repository.GetByIdAsync(id);

            if (productType == null)
                return null;

            var result = new ProductTypeDto
            {
                Id = productType.Id,
                Name = productType.Name,
                //Description = productType.Description
            };

            _cache.Set(
                cacheKey,
                result,
                TimeSpan.FromMinutes(10));

            return result;
        }

        public async Task<ProductTypeDto> CreateAsync(
            ProductTypeDto productTypeDto)
        {
            var productType = new ProductType
            {
                Id = Guid.NewGuid(),
                Name = productTypeDto.Name,
                //Description = productTypeDto.Description
            };

            var createdProductType =
                await _repository.CreateAsync(productType);

            _cache.Remove(CacheKey);

            return new ProductTypeDto
            {
                Id = createdProductType.Id,
                Name = createdProductType.Name,
                //Description = createdProductType.Description
            };
        }

        public async Task<bool> UpdateAsync(
            Guid id,
            ProductTypeDto product)
        {
            var productType = new ProductType
            {
                Id = id,
                Name = product.Name,
                //Description = product.Description
            };

            var result = await _repository.UpdateAsync(
                id,
                productType);

            if (result)
            {
                _cache.Remove(CacheKey);
                _cache.Remove($"product_type_{id}");
            }

            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _repository.DeleteAsync(id);

            if (result)
            {
                _cache.Remove(CacheKey);
                _cache.Remove($"product_type_{id}");
            }

            return result;
        }
    }
}