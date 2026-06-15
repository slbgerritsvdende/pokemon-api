using Microsoft.EntityFrameworkCore;
using Pokedex.Domain.Entities;
using Pokedex.Domain.Interfaces;
using Pokedex.Infrastructure.Data;

namespace Pokedex.Infrastructure.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private readonly PokedexDbContext _context;

    public PokemonRepository(PokedexDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pokemon>> GetAllAsync(int page, int pageSize)
        => await _context.Pokemon
            .Include(p => p.Types)
            .OrderBy(p => p.PokedexNumber)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async Task<Pokemon?> GetByIdAsync(int id)
        => await _context.Pokemon
            .Include(p => p.Types)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Pokemon>> GetByTypeAsync(string type)
        => await _context.Pokemon
            .Include(p => p.Types)
            .Where(p => p.Types.Any(t => t.TypeName.ToLower() == type.ToLower()))
            .ToListAsync();

    public async Task<Pokemon> AddAsync(Pokemon pokemon)
    {
        _context.Pokemon.Add(pokemon);
        await _context.SaveChangesAsync();
        return pokemon;
    }

    public async Task<Pokemon?> UpdateAsync(int id, Pokemon updated)
    {
        var existing = await _context.Pokemon
            .Include(p => p.Types)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (existing is null) return null;

        existing.Name = updated.Name;
        existing.Description = updated.Description;
        existing.HeightInMeters = updated.HeightInMeters;
        existing.WeightInKg = updated.WeightInKg;

        // Replace types
        _context.PokemonTypes.RemoveRange(existing.Types);
        existing.Types = updated.Types;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pokemon = await _context.Pokemon.FindAsync(id);
        if (pokemon is null) return false;

        _context.Pokemon.Remove(pokemon);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetTotalCountAsync()
        => await _context.Pokemon.CountAsync();
}
