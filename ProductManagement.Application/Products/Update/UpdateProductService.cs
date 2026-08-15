using FluentValidation;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Services;

namespace ProductManagement.Application.Products.Update;

public class UpdateProductService : IUpdateProductService
{
    private readonly IProductRepository _repository;
    private readonly ProductDomainService _productDomainService;
    private readonly IValidator<UpdateProductDto> _validator;
    public UpdateProductService(
        IProductRepository repository,
        ProductDomainService productDomainService, IValidator<UpdateProductDto> validator)
    {
        _repository = repository;
        _productDomainService = productDomainService;
        _validator = validator;
    }

    public async Task<bool> UpdateAsync(
        Guid id,
        UpdateProductDto dto,
        CancellationToken cancellationToken = default)
    {
        await _validator.ValidateAndThrowAsync(
    dto,
    cancellationToken);

        var product = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        if (product == null)
            return false;

        _productDomainService.UpdateProduct(
            product,
            dto.Name,
            dto.Description,
            dto.Price,
            dto.Quantity);

        await _repository.UpdateAsync(
            product,
            cancellationToken);

        return true;
    }
}