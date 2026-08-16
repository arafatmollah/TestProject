using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public List<OrderItemDto> Items { get; set; } = new();
    }
}
