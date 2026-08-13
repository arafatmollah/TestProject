using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Domain.Services
{
    public class RegistrationService
    {
        public User Register(
     string firstName,
     string lastName,
     string phone,
     string email,
     string passwordHash)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Phone = phone,
                Email = email,
                PasswordHash = passwordHash,
                Role = "User",
                CreatedAt = DateTime.UtcNow
            };
        }

    

        //public User Register(string email, string passwordHash)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
