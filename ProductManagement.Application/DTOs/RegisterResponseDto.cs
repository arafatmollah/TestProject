using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.DTOs
{
    public class RegisterResponseDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
