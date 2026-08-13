using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.DTOs
{
    public class LogInResponseDto
    {
            public string Email { get; set; } = string.Empty;

            public string Token { get; set; } = string.Empty;
        
    }
}
