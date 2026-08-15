using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.ProductType.DeleteProductType
{
    public interface IDeleteProductType
    {
        Task<bool> DeleteAsync(Guid id);
    }
}
