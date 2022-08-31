using Coffeeshop.Api.Dto;
using Coffeeshop.Domain.Queries;
using FluentValidation;
using MediatR;

namespace Coffeeshop.Api.Validations;

public class OrderCreateValidator : AbstractValidator<OrderCreateDto>
{
    public OrderCreateValidator(IValidator<OrderCreateItemDto> orderCreateItemValidator)
    {
        _orderCreateItemValidator = orderCreateItemValidator;
        RuleFor(c => c.Name).NotEmpty().MaximumLength(50);
        _ = RuleFor(c => c.Items).NotEmpty().NotNull().CustomAsync(ValidateItemsAsync);
    }

    private readonly IValidator<OrderCreateItemDto> _orderCreateItemValidator;

    private async Task ValidateItemsAsync(IEnumerable<OrderCreateItemDto> items, ValidationContext<OrderCreateDto> validationContext, CancellationToken cancellationToken)
    {
        if (items != null)
        {
            foreach (var item in items)
            {
                bool isValid = (await _orderCreateItemValidator.ValidateAsync(item)).IsValid;
                if(!isValid)
                {
                    validationContext.AddFailure(nameof(OrderCreateDto.Items), "Items are invalid");
                }
            }
        }
    }    
}

public class OrderCreateItemValidator : AbstractValidator<OrderCreateItemDto>
{
    public OrderCreateItemValidator(IMediator mediator)
    {
        _mediator = mediator;

        RuleFor(c => c.Quantity).GreaterThan(0);
        RuleFor(c => c.CoffeeId).MustAsync(ValidateCoffeeExistsAsync);
    }

    private readonly IMediator _mediator;

    private async Task<bool> ValidateCoffeeExistsAsync(long coffeeId, CancellationToken cancellationToken)
    {
        var coffee = await _mediator.Send(new CoffeeGet.Query(coffeeId));
        return coffee != null;
    }
}
