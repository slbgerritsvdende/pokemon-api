using FluentValidation;
using Pokedex.Application.DTOs;

namespace Pokedex.Application.Validators;

public class UpdatePokemonValidator : AbstractValidator<UpdatePokemonDto>
{
    public UpdatePokemonValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("A Pokémon must have a name!")
            .MaximumLength(100);

        RuleFor(x => x.HeightInMeters)
            .GreaterThan(0);

        RuleFor(x => x.WeightInKg)
            .GreaterThan(0);

        RuleFor(x => x.Types)
            .NotEmpty().WithMessage("A Pokémon must have at least one type!")
            .Must(t => t.Count() <= 2).WithMessage("A Pokémon cannot have more than 2 types!");
    }
}