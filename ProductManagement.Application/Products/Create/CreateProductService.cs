using AutoMapper;
using FluentValidation;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Services;

namespace ProductManagement.Application.Products.Create;

public class CreateProductService : ICreateProductService
{
    private readonly IProductRepository _repository;
    private readonly ProductDomainService _productDomainService;
    private readonly IValidator<CreateProductDto> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductService(
        IProductRepository repository,
        ProductDomainService productDomainService,
        IValidator<CreateProductDto> validator,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _productDomainService = productDomainService;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDto> CreateAsync(
        CreateProductDto dto,
        CancellationToken cancellationToken = default)
    {
        await _validator.ValidateAndThrowAsync(
            dto,
            cancellationToken);

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

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return _mapper.Map<ProductDto>(product);
    }
}