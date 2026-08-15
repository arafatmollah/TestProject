using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.ProductType.UpdateProductType
{
    public interface IUpdateProductTypeService
    {
        Task<bool> UpdateAsync(Guid id, ProductTypeDto product);
    }
}
