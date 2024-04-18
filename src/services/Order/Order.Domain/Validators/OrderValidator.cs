using FluentValidation;
using Order.Domain.Entities;

namespace Order.Domain.Validators;

public class OrderValidator : AbstractValidator<OrderEntity> {
    public OrderValidator() {

        RuleFor(x => x.OrderStatus)
            .NotEmpty().WithMessage("The orders status can't be empty.")
            .NotNull().WithMessage("The order status can't be null.")
            .MaximumLength(80).WithMessage("The order status must have the maximum of one hundred characters.");

        RuleFor(x => x.Person)
            .NotEmpty().WithMessage("The user can't be empty.")
            .NotNull().WithMessage("The user can't be null.");
    }
}