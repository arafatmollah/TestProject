using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.DTOs
{
    public class ProductTypeDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
