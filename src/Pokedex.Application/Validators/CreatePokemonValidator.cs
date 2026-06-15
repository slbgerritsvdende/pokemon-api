using FluentValidation;
using Pokedex.Application.DTOs;

namespace Pokedex.Application.Validators;

public class CreatePokemonValidator : AbstractValidator<CreatePokemonDto>
{
    public CreatePokemonValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("A Pokémon must have a name!")
            .MaximumLength(100);

        RuleFor(x => x.PokedexNumber)
            .GreaterThan(0).WithMessage("Pokédex number must be positive!");

        RuleFor(x => x.HeightInMeters)
            .GreaterThan(0).WithMessage("Height must be greater than zero!");

        RuleFor(x => x.WeightInKg)
            .GreaterThan(0).WithMessage("Weight must be greater than zero!");

        RuleFor(x => x.Types)
            .NotEmpty().WithMessage("A Pokémon must have at least one type!")
            .Must(t => t.Count() <= 2).WithMessage("A Pokémon cannot have more than 2 types!");
    }
}