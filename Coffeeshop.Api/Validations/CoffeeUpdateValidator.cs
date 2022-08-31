using Coffeeshop.Api.Dto;
using FluentValidation;

namespace Coffeeshop.Api.Validations;

public class CoffeeUpdateValidator : AbstractValidator<CoffeeUpdateDto>
{
    public CoffeeUpdateValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(50);
        RuleFor(c => c.Type).NotEmpty().MaximumLength(25);
        RuleFor(c => c.Quantity).GreaterThanOrEqualTo(0);
    }
}
