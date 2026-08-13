using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Domain.Services
{
    public class RegistrationService
    {
        public User Register(
        string email,
        string passwordHash)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                PasswordHash = passwordHash,
                Role = "User",
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
