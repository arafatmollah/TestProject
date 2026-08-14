using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Application.Products.GetById;

public class GetProductByIdService : IGetProductByIdService
{
    private readonly IProductRepository _repository;

    public GetProductByIdService(
        IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var product = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (product == null)
            return null;

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.Quantity,
            ProductTypeId = product.ProductTypeId,
            ProductTypeName =
                product.ProductType?.Name ?? string.Empty,
            ExpirationDate =
                product.ProductExpiration?.ExpirationDate,
            Tags = product.ProductTags
                .Select(t => t.Name)
                .ToList()
        };
    }
}