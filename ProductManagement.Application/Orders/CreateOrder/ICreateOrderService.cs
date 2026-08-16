using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.Orders.CreateOrder
{
    public interface ICreateOrderService
    {
        Task<OrderDto> CreateAsync(
        CreateOrderDto dto,
        Guid userId,
        CancellationToken cancellationToken = default);
    }
}
