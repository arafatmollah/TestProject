using AutoMapper;
using FluentValidation;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Exceptions;
using ProductManagement.Domain.Services;

namespace ProductManagement.Application.Products.Update;

public class UpdateProductService : IUpdateProductService
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateProductDto> _validator;

    public UpdateProductService(
        IProductRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<UpdateProductDto> validator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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

        _mapper.Map(dto, product);

        await _repository.UpdateAsync(
            product,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}