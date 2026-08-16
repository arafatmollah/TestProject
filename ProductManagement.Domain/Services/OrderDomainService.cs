using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Enums;
using ProductManagement.Domain.Exceptions;

namespace ProductManagement.Domain.Services;

public class OrderDomainService
{
    public Order CreateOrder(
        Guid userId,
        List<(Product Product, int Quantity)> items)
    {
        if (userId == Guid.Empty)
        {
            throw new BusinessRuleException(
                "User is required.");
        }

        if (items == null || items.Count == 0)
        {
            throw new BusinessRuleException(
                "Order must contain at least one item.");
        }

        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Pending
        };

        foreach (var item in items)
        {
            if (item.Product == null)
            {
                throw new BusinessRuleException(
                    "Product is required.");
            }

            if (item.Quantity <= 0)
            {
                throw new BusinessRuleException(
                    "Order quantity must be greater than zero.");
            }

            if (item.Product.Quantity < item.Quantity)
            {
                throw new BusinessRuleException(
                    $"Insufficient stock for product '{item.Product.Name}'.");
            }

            var orderItem = new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProductId = item.Product.Id,
                Quantity = item.Quantity,
                UnitPrice = item.Product.Price,
                TotalPrice = item.Product.Price * item.Quantity
            };

            order.OrderItems.Add(orderItem);

            item.Product.Quantity -= item.Quantity;

            order.TotalAmount += orderItem.TotalPrice;
        }

        return order;
    }
}