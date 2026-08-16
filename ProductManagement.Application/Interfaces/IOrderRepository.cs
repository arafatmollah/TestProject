using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(
        Order order,
        CancellationToken cancellationToken = default);

    Task<Order?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}