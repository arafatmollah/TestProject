using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Exceptions;

namespace ProductManagement.Domain.Services;

public class ProductTypeDomainService
{
    public ProductType CreateProductType(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BusinessRuleException(
                "Product type name is required.");
        }

        return new ProductType
        {
            Id = Guid.NewGuid(),
            Name = name.Trim()
        };
    }

    public void UpdateProductType(
        ProductType productType,
        string name)
    {
        if (productType == null)
        {
            throw new BusinessRuleException(
                "Product type is required.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BusinessRuleException(
                "Product type name is required.");
        }

        productType.Name = name.Trim();
    }
}