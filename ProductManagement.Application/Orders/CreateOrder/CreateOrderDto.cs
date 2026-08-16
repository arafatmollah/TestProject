using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.Orders.CreateOrder
{
    public class CreateOrderDto
    {
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}
