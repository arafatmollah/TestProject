using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Services;

namespace ProductManagement.Application.Products.Create;

public class CreateProductService : ICreateProductService
{
    private readonly IProductRepository _repository;
    private readonly ProductDomainService _productDomainService;

    public CreateProductService(
        IProductRepository repository,
        ProductDomainService productDomainService)
    {
        _repository = repository;
        _productDomainService = productDomainService;
    }

    public async Task<ProductDto> CreateAsync(
        ProductDto dto,
        CancellationToken cancellationToken = default)
    {
        var product = _productDomainService.CreateProduct(
            dto.Name,
            dto.Description,
            dto.Price,
            dto.Quantity,
            dto.ProductTypeId);

        if (dto.ExpirationDate.HasValue)
        {
            product.ProductExpiration = new ProductExpiration
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                ExpirationDate = dto.ExpirationDate.Value
            };
        }

        foreach (var tag in dto.Tags)
        {
            product.ProductTags.Add(new ProductTag
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Name = tag
            });
        }

        await _repository.AddAsync(
            product,
            cancellationToken);

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.Quantity,
            ProductTypeId = product.ProductTypeId,
            ExpirationDate =
                product.ProductExpiration?.ExpirationDate,
            Tags = product.ProductTags
                .Select(x => x.Name)
                .ToList()
        };
    }
}