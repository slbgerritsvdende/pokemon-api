using Pokedex.Application.DTOs;

namespace Pokedex.Application.Interfaces;

public interface IPokemonService
{
    Task<PagedResultDto<PokemonDto>> GetAllAsync(int page = 1, int pageSize = 20);
    Task<PokemonDto?> GetByIdAsync(int id);
    Task<IEnumerable<PokemonDto>> GetByTypeAsync(string type);
    Task<PokemonDto> CreateAsync(CreatePokemonDto dto);
    Task<PokemonDto?> UpdateAsync(int id, UpdatePokemonDto dto);
    Task<bool> DeleteAsync(int id);
}
