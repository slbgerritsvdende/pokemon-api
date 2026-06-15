namespace Pokedex.Application.DTOs;

// What we RETURN to callers
public record PokemonDto(
    int Id,
    int PokedexNumber,
    string Name,
    string Description,
    double HeightInMeters,
    double WeightInKg,
    IEnumerable<string> Types
);

// What callers SEND us to create a Pokémon
public record CreatePokemonDto(
    int PokedexNumber,
    string Name,
    string Description,
    double HeightInMeters,
    double WeightInKg,
    IEnumerable<string> Types
);

// What callers SEND us to update a Pokémon
public record UpdatePokemonDto(
    string Name,
    string Description,
    double HeightInMeters,
    double WeightInKg,
    IEnumerable<string> Types
);

// Paginated response wrapper
public record PagedResultDto<T>(
    IEnumerable<T> Items,
    int TotalCount,
    int Page,
    int PageSize
);
