namespace Pokedex.Domain.Entities;

public class Pokemon
{
    public int Id { get; set; }
    public int PokedexNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double HeightInMeters { get; set; }
    public double WeightInKg { get; set; }

    // Navigation properties
    public ICollection<PokemonType> Types { get; set; } = new List<PokemonType>();
}
