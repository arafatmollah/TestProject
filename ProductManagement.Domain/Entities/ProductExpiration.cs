using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Domain.Entities
{
    public class ProductExpiration
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Product Product { get; set; } = null!;
    }
}
