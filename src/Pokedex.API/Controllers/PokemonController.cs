using Microsoft.AspNetCore.Mvc;
using Pokedex.Application.DTOs;
using Pokedex.Application.Interfaces;

namespace Pokedex.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PokemonController : ControllerBase
{
    private readonly IPokemonService _service;

    public PokemonController(IPokemonService service)
    {
        _service = service;
    }

    /// <summary>Get all Pokémon, paginated</summary>
    [HttpGet]
    public async Task<ActionResult<PagedResultDto<PokemonDto>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _service.GetAllAsync(page, pageSize);
        return Ok(result);
    }

    /// <summary>Get a specific Pokémon by ID</summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PokemonDto>> GetById(int id)
    {
        var pokemon = await _service.GetByIdAsync(id);
        return pokemon is null ? NotFound($"Pokémon #{id} not found in the Pokédex!") : Ok(pokemon);
    }

    /// <summary>Get all Pokémon of a specific type</summary>
    [HttpGet("type/{type}")]
    public async Task<ActionResult<IEnumerable<PokemonDto>>> GetByType(string type)
    {
        var pokemon = await _service.GetByTypeAsync(type);
        return Ok(pokemon);
    }

    /// <summary>Add a new Pokémon to the Pokédex</summary>
    [HttpPost]
    public async Task<ActionResult<PokemonDto>> Create([FromBody] CreatePokemonDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>Update an existing Pokémon</summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<PokemonDto>> Update(int id, [FromBody] UpdatePokemonDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return updated is null ? NotFound($"Pokémon #{id} not found in the Pokédex!") : Ok(updated);
    }

    /// <summary>Release a Pokémon from the Pokédex</summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound($"Pokémon #{id} not found in the Pokédex!");
    }
}
