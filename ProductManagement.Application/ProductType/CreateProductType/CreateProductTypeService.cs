using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Services;

namespace ProductManagement.Application.ProductType.CreateProductType;

public class CreateProductTypeService : ICreateProductType
{
    private readonly IProductTypeRepository _repository;
    private readonly ProductTypeDomainService _productTypeDomainService;

    public CreateProductTypeService(
        IProductTypeRepository repository,
        ProductTypeDomainService productTypeDomainService)
    {
        _repository = repository;
        _productTypeDomainService = productTypeDomainService;
    }

    public async Task<ProductTypeDto> CreateAsync(
        ProductTypeDto dto,
        CancellationToken cancellationToken = default)
    {
        var productType =
            _productTypeDomainService.CreateProductType(
                dto.Name);

        var createdProductType =
            await _repository.CreateAsync(
                productType);

        return new ProductTypeDto
        {
            Id = createdProductType.Id,
            Name = createdProductType.Name
        };
    }
}