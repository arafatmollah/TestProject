using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.DTOs;

public class ProductDto
{
   
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }


        public Guid ProductTypeId { get; set; }


        public string ProductTypeName { get; set; } = string.Empty;

        public DateTime? ExpirationDate { get; set; }

        public List<string> Tags { get; set; } = new();
    
}