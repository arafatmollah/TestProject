using ProductManagement.Domain.Entities;

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
            throw new InvalidOperationException(
                "Product name is required.");
        }

        if (price < 0)
        {
            throw new InvalidOperationException(
                "Product price cannot be negative.");
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
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException(
                "Product name is required.");
        }

        if (price < 0)
        {
            throw new InvalidOperationException(
                "Product price cannot be negative.");
        }

        if (quantity < 0)
        {
            throw new InvalidOperationException(
                "Product quantity cannot be negative.");
        }

        product.Name = name;
        product.Description = description;
        product.Price = price;
        product.Quantity = quantity;
    }
}