namespace ProductManagement.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public Guid ProductTypeId { get; set; }

    public ProductType ProductType { get; set; } = null!;

    public ProductExpiration? ProductExpiration { get; set; }

    public ICollection<ProductTag> ProductTags { get; set; }
        = new List<ProductTag>();

    public DateTime CreatedAt { get; set; }
}