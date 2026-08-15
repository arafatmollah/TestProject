using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.ProductType.GetProductType
{
    public class GetProductTypeService : IGetProductType
    {
        private readonly IProductTypeRepository _repository;

        public GetProductTypeService(IProductTypeRepository productTypeRepository)
        {
            _repository = productTypeRepository;
        }
        public async Task<List<ProductTypeDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            var result = products.Select(p => new ProductTypeDto
            {
                Id = p.Id,
                Name = p.Name,
               
            }).ToList();

            return result;
        }
    }
}
