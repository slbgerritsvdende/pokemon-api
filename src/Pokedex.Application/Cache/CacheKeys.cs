namespace Pokedex.Application.Cache;

public static class CacheKeys
{
    public static string AllPokemon(int page, int pageSize) 
        => $"pokemon:all:{page}:{pageSize}";
    
    public static string PokemonById(int id)        
        => $"pokemon:id:{id}";
    
    public static string PokemonByType(string type) 
        => $"pokemon:type:{type.ToLower()}";
}