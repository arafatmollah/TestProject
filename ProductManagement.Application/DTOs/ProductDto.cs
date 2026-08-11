using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.DTOs;

public class ProductDto
{
   
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        // Used when creating/updating
        public Guid ProductTypeId { get; set; }

        // Used in response
        public string ProductTypeName { get; set; } = string.Empty;

        public DateTime? ExpirationDate { get; set; }

        public List<string> Tags { get; set; } = new();
    
}