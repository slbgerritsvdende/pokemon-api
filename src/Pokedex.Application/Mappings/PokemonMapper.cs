using Pokedex.Application.DTOs;
using Pokedex.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Pokedex.Application.Mappings;

[Mapper]
public partial class PokemonMapper
{
    // Pokemon → PokemonDto
    // Mapperly needs a hint for the Types collection flattening
    public partial PokemonDto ToDto(Pokemon pokemon);

    private static IEnumerable<string> MapTypes(ICollection<PokemonType> types)
        => types.Select(t => t.TypeName);

    // CreatePokemonDto → Pokemon
    public partial Pokemon ToEntity(CreatePokemonDto dto);

    private static ICollection<PokemonType> MapTypeNames(IEnumerable<string> types)
        => types.Select(t => new PokemonType { TypeName = t }).ToList();

    // UpdatePokemonDto → Pokemon
    public partial Pokemon ToEntity(UpdatePokemonDto dto);
}