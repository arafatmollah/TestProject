using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.ProductType.UpdateProductType
{
    internal class UpdateProductTypeService : IUpdateProductTypeService
    {
        private readonly IProductTypeRepository _repository;
        private readonly ProductTypeDomainService _productTypeDomainService;
        public UpdateProductTypeService(IProductTypeRepository repository, ProductTypeDomainService productTypeDomainService)
        {
            _repository = repository;
            _productTypeDomainService = productTypeDomainService;
        }
        public async Task<bool> UpdateAsync(Guid id, ProductTypeDto dto)
        {
            var productType = await _repository.GetByIdAsync(
             id
             );

            if (productType == null)
                return false;

            _productTypeDomainService.UpdateProductType(
                productType,
                dto.Name
               );

            return true;
        }
    }
}
