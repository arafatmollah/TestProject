using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.ProductType.GetByIdProductType
{
    public interface IGetProductTypeById
    {
        Task<ProductTypeDto?> GetByIdAsync(Guid id);
    }
}
