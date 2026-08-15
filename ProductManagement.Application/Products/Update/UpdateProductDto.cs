namespace ProductManagement.Application.Products.Update;

public class UpdateProductDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public Guid ProductTypeId { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public List<string> Tags { get; set; } = new();
}