using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using System;

namespace ProductManagement.Application.ProductType.GetByIdProductType
{
    public class GetProductTypeByIdService : IGetProductTypeById
    {
        private readonly IProductTypeRepository _productType;

        public GetProductTypeByIdService(IProductTypeRepository productType)
        {
            _productType = productType;
        }

        public async Task<ProductTypeDto?> GetByIdAsync(Guid id)
        {
            var productType = await _productType.GetByIdAsync(id);

            if (productType == null)
                return null;

            return new ProductTypeDto
            {
                Id = productType.Id,
                Name = productType.Name
            };
        }
    }
}