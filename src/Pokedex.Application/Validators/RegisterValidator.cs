using FluentValidation;
using Pokedex.Application.DTOs;

namespace Pokedex.Application.Validators;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Username must be at least 3 characters!");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress().WithMessage("A valid email is required!");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8).WithMessage("Password must be at least 8 characters!")
            .Matches("[A-Z]").WithMessage("Password must contain an uppercase letter!")
            .Matches("[0-9]").WithMessage("Password must contain a number!");
    }
}