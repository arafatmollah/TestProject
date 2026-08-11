using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Domain.Entities
{
    public class ProductTag
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public Product Product { get; set; } = null!;
    }
}
