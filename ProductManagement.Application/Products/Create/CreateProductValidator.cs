using FluentValidation;

namespace ProductManagement.Application.Products.Create;

public class CreateProductValidator
    : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Product description is required.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product price cannot be negative.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product quantity cannot be negative.");

        RuleFor(x => x.ProductTypeId)
            .NotEmpty()
            .WithMessage("Product type is required.");

        RuleForEach(x => x.Tags)
            .NotEmpty()
            .WithMessage("Tag cannot be empty.");
    }
}