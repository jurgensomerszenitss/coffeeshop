using Coffeeshop.Api.Dto;
using FluentValidation;

namespace Coffeeshop.Api.Validations;

public class OrderUpdateValidator : AbstractValidator<OrderUpdateDto>
{
    public OrderUpdateValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(50);
        RuleFor(c => c.Items).NotEmpty().NotNull();
    }
}
