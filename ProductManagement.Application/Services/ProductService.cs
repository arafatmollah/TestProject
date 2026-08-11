using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProductDto>> GetAllAsync(
        string? search)
    {
        var products = await _repository.GetAllAsync(search);

        return products.Select(x => new ProductDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Price = x.Price
        }).ToList();
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
            return null;

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        };
    }

    public async Task<ProductDto> CreateAsync(ProductDto dto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(product);

        dto.Id = product.Id;

        return dto;
    }

    public async Task<bool> UpdateAsync(
        Guid id,
        ProductDto dto)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
            return false;

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;

        await _repository.UpdateAsync(product);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
            return false;

        await _repository.DeleteAsync(product);

        return true;
    }
}