using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Application.Orders.CreateOrder
{
    public class CreateOrderItemValidator:AbstractValidator<CreateOrderItemDto>
    {
        public CreateOrderItemValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("Product is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");
        }
    }
}
