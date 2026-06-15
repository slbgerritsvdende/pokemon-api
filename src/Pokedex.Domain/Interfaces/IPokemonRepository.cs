using Pokedex.Domain.Entities;

namespace Pokedex.Domain.Interfaces;

public interface IPokemonRepository
{
    Task<IEnumerable<Pokemon>> GetAllAsync(int page, int pageSize);
    Task<Pokemon?> GetByIdAsync(int id);
    Task<IEnumerable<Pokemon>> GetByTypeAsync(string type);
    Task<Pokemon> AddAsync(Pokemon pokemon);
    Task<Pokemon?> UpdateAsync(int id, Pokemon pokemon);
    Task<bool> DeleteAsync(int id);
    Task<int> GetTotalCountAsync();
}
