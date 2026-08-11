using System;
using System.Collections.Generic;
using System.Text;
using global::ProductManagement.Domain.Entities;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Interfaces
{


    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(
            string email,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            User user,
            CancellationToken cancellationToken = default);
    }
}
