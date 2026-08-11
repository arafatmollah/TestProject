using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Common;
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
        decimal? price,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductTags)
            .Include(p => p.ProductExpiration)
            .AsQueryable();

        // Search
        if (!string.IsNullOrWhiteSpace(search))
        {
            var normalizedSearch =
                SearchHelper.Normalize(search);

            query = query.Where(p =>
                p.Name
                    .ToLower()
                    .Replace(" ", "")
                    .Contains(normalizedSearch)

                ||

                p.Description
                    .ToLower()
                    .Replace(" ", "")
                    .Contains(normalizedSearch)

                ||

                p.ProductType.Name
                    .ToLower()
                    .Replace(" ", "")
                    .Contains(normalizedSearch)

                ||

                p.ProductTags.Any(tag =>
                    tag.Name
                        .ToLower()
                        .Replace(" ", "")
                        .Contains(normalizedSearch))
            );
        }

        // Price
        if (price.HasValue)
        {
            query = query.Where(p =>
                p.Price == price.Value);
        }

        return await query.ToListAsync(
            cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductTags)
            .Include(p => p.ProductExpiration)
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