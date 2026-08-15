using ProductManagement.Application.DTOs;
using ProductManagement.Application.Products.Create;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.ProductType.CreateProductType
{
    public interface ICreateProductType
    {
        Task<ProductTypeDto> CreateAsync(
        ProductTypeDto dto,
        CancellationToken cancellationToken = default);
    }
}
