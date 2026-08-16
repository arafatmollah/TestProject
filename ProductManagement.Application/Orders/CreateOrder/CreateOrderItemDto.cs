using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.Orders.CreateOrder
{
    public class CreateOrderItemDto
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
