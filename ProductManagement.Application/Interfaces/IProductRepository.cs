using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync(
     string? search,
     string? productType,
     decimal? price,
     CancellationToken cancellationToken = default);

    Task<Product?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Product product,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Product product,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Product product,
        CancellationToken cancellationToken = default);
}