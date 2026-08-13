using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductManagement.Application.DTOs
{

    public class RegisterDto
    {
       
            public string FirstName { get; set; } = string.Empty;

            public string LastName { get; set; } = string.Empty;

            public string Email { get; set; } = string.Empty;

            public string Password { get; set; } = string.Empty;
       
    }
}
