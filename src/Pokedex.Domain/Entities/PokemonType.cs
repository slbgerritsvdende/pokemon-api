namespace Pokedex.Domain.Entities;

public class PokemonType
{
    public int Id { get; set; }
    public int PokemonId { get; set; }
    public string TypeName { get; set; } = string.Empty;

    // Navigation property
    public Pokemon Pokemon { get; set; } = null!;
}
