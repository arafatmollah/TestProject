using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.DTOs
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
