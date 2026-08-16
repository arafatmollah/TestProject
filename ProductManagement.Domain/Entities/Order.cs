using ProductManagement.Domain.Enums;

namespace ProductManagement.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public OrderStatus Status { get; set; }

    public decimal TotalAmount { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new();
}