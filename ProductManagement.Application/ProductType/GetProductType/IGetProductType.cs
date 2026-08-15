using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.ProductType.GetProductType
{
    public interface IGetProductType
    {
        Task<List<ProductTypeDto>> GetAllAsync();
    }
}
