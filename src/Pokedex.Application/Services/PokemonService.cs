using Microsoft.Extensions.Caching.Memory;
using Pokedex.Application.Cache;
using Pokedex.Application.DTOs;
using Pokedex.Application.Interfaces;
using Pokedex.Application.Mappings;
using Pokedex.Domain.Entities;
using Pokedex.Domain.Interfaces;

namespace Pokedex.Application.Services;

public class PokemonService(IPokemonRepository repository, PokemonMapper mapper, IMemoryCache cache) : IPokemonService
{
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);
    
    public async Task<PagedResultDto<PokemonDto>> GetAllAsync(int page = 1, int pageSize = 20)
    {
        var key = CacheKeys.AllPokemon(page, pageSize);

        return await cache.GetOrCreateAsync(key, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheDuration;

            var pokemon = await repository.GetAllAsync(page, pageSize);
            var total = await repository.GetTotalCountAsync();

            return new PagedResultDto<PokemonDto>(
                Items: pokemon.Select(mapper.ToDto),
                TotalCount: total,
                Page: page,
                PageSize: pageSize
            );
        }) ?? throw new InvalidOperationException("Failed to retrieve Pokémon from cache.");
    }

    public async Task<PokemonDto?> GetByIdAsync(int id)
    {
        var pokemon = await repository.GetByIdAsync(id);
        return pokemon is null ? null : mapper.ToDto(pokemon);
    }

    public async Task<IEnumerable<PokemonDto>> GetByTypeAsync(string type)
    {
        var pokemon = await repository.GetByTypeAsync(type);
        return pokemon.Select(mapper.ToDto);
    }

    public async Task<PokemonDto> CreateAsync(CreatePokemonDto dto)
    {
        var pokemon = new Pokemon
        {
            PokedexNumber = dto.PokedexNumber,
            Name = dto.Name,
            Description = dto.Description,
            HeightInMeters = dto.HeightInMeters,
            WeightInKg = dto.WeightInKg,
            Types = dto.Types.Select(t => new PokemonType { TypeName = t }).ToList()
        };

        var created = await repository.AddAsync(pokemon);
        return mapper.ToDto(created);
    }

    public async Task<PokemonDto?> UpdateAsync(int id, UpdatePokemonDto dto)
    {
        var updated = new Pokemon
        {
            Name = dto.Name,
            Description = dto.Description,
            HeightInMeters = dto.HeightInMeters,
            WeightInKg = dto.WeightInKg,
            Types = dto.Types.Select(t => new PokemonType { TypeName = t }).ToList()
        };

        var result = await repository.UpdateAsync(id, updated);
        return result is null ? null : mapper.ToDto(result);
    }

    public async Task<bool> DeleteAsync(int id)
        => await repository.DeleteAsync(id);
}
