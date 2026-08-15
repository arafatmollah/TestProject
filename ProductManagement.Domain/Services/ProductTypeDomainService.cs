using Microsoft.AspNetCore.Http.HttpResults;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Domain.Services
{
    public class ProductTypeDomainService
    {
        public ProductType CreateProductType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException(
                    "Product type name is required.");
            }

            return new ProductType
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }
        public void UpdateProductType(
    ProductType product,
    string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException(
                    "Product name is required.");
            }
            product.Name = name;
        }
    }
}
