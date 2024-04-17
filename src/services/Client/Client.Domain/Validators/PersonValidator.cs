using Client.Domain.Entities;
using FluentValidation;

namespace Client.Domain.Validators;
public class PersonValidator : AbstractValidator<Person> {
    public PersonValidator() {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The name can't be empty.")
            .NotNull().WithMessage("The name can't be null.")
            .MaximumLength(80).WithMessage("The name must have the maximum of one hundred characters.")
            .MinimumLength(2).WithMessage("The name must have at least two characters.");

        RuleFor(x => x.Age)
            .NotEmpty().WithMessage("The age can't be empty.")
            .NotNull().WithMessage("The age can't be null.");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("The role can't be empty.")
            .NotNull().WithMessage("The role can't be null.")
            .MaximumLength(80).WithMessage("The role must have the maximum of one hundred characters.")
            .Must(RoleValidation).WithMessage("The role values must be only 'user' or 'admin'.")
            .MinimumLength(2).WithMessage("The role must have at least two characters.");

        RuleFor(x => x.Document)
            .NotEmpty().WithMessage("The document can't be empty.")
            .NotNull().WithMessage("The document can't be null.")
            .MaximumLength(80).WithMessage("The document must have the maximum of one hundred characters.")
            .MinimumLength(2).WithMessage("The document must have at least two characters.");
    }
     private bool RoleValidation(string cnhType) {
            var list = new  List<string> {"user", "admin"};
            return list.Contains(cnhType);
        }
}