using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Exceptions;

namespace ProductManagement.Domain.Services;

public class ProductDomainService
{
    public Product CreateProduct(
        string name,
        string description,
        decimal price,
        int quantity,
        Guid productTypeId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BusinessRuleException(
                "Product name is required.");
        }

        if (price < 0)
        {
            throw new BusinessRuleException(
                "Product price cannot be negative.");
        }

        if (quantity < 0)
        {
            throw new BusinessRuleException(
                "Product quantity cannot be negative.");
        }

        if (productTypeId == Guid.Empty)
        {
            throw new BusinessRuleException(
                "Product type is required.");
        }

        return new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Price = price,
            Quantity = quantity,
            ProductTypeId = productTypeId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateProduct(
        Product product,
        string name,
        string description,
        decimal price,
        int quantity)
    {
        if (product == null)
        {
            throw new BusinessRuleException(
                "Product is required.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BusinessRuleException(
                "Product name is required.");
        }

        if (price < 0)
        {
            throw new BusinessRuleException(
                "Product price cannot be negative.");
        }

        if (quantity < 0)
        {
            throw new BusinessRuleException(
                "Product quantity cannot be negative.");
        }

        product.Name = name;
        product.Description = description;
        product.Price = price;
        product.Quantity = quantity;
    }
}