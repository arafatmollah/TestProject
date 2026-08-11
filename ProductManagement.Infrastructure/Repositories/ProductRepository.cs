using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Data;

namespace ProductManagement.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllAsync(
        string? search,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p =>
                p.Name.Contains(search));
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .FirstOrDefaultAsync(
                p => p.Id == id,
                cancellationToken);
    }

    public async Task AddAsync(
        Product product,
        CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(
            product,
            cancellationToken);

        await _context.SaveChangesAsync(
            cancellationToken);
    }

    public async Task UpdateAsync(
        Product product,
        CancellationToken cancellationToken = default)
    {
        _context.Products.Update(product);

        await _context.SaveChangesAsync(
            cancellationToken);
    }

    public async Task DeleteAsync(
        Product product,
        CancellationToken cancellationToken = default)
    {
        _context.Products.Remove(product);

        await _context.SaveChangesAsync(
            cancellationToken);
    }
}