using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Common;
using ProductManagement.Application.Common.Pagination;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Products.Get;
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

    public async Task<PagedResult<Product>> GetAllAsync(
        ProductFilter filter,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductTags)
            .Include(p => p.ProductExpiration)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var normalizedSearch =
                SearchHelper.Normalize(filter.Search);

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
        if (!string.IsNullOrWhiteSpace(filter.ProductType))
        {
            var normalizedSearch =
               SearchHelper.Normalize(filter.ProductType);

            query = query.Where(p => p.ProductType.Name
                    .ToLower()
                    .Replace(" ", "")
                    .Contains(normalizedSearch));
        }

        //if (price.HasValue)
        //{
        //    query = query.Where(p =>
        //        p.Price == price.Value);
        //}
        if (filter.MinPrice.HasValue)
        {
            query = query.Where(p =>
                p.Price >= filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(p =>
                p.Price <= filter.MaxPrice.Value);
        }
        var totalCount = await query.CountAsync(
    cancellationToken);

        var products = await query
    .OrderBy(p => p.Name)
    .Skip((filter.Page - 1) * filter.PageSize)
    .Take(filter.PageSize)
    .ToListAsync(cancellationToken);

        return new PagedResult<Product>
        {
            Items = products,
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(
                totalCount / (double)filter.PageSize)
        };

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
    }

    public async Task UpdateAsync(
    Product product,
    CancellationToken cancellationToken = default)
    {
        _context.Products.Update(product);
    }

    public async Task DeleteAsync(
    Product product,
    CancellationToken cancellationToken = default)
    {
        _context.Products.Remove(product);
    }
}