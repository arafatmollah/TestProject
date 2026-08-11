using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Domain.Entities;

public class ProductType
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; }
        = new List<Product>();
}
