using FluentValidation;
using Pokedex.Application.DTOs;
using Pokedex.Application.Interfaces;

namespace Pokedex.API.Endpoints;

public static class PokemonEndpoints
{
    public static void MapPokemonEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/pokemon").WithTags("Pokemon");

        group.MapGet("/", async (IPokemonService service,
                int page = 1,
                int pageSize = 20) =>
            {
                var result = await service.GetAllAsync(page, pageSize);
                return Results.Ok(result);
            }).WithName("GetAllPokemon")
            .RequireAuthorization();

        group.MapGet("/{id}", async (IPokemonService service, int id) =>
            {
                var result = await service.GetByIdAsync(id);
                return Results.Ok(result);
            }).WithName("GetPokemonById")
            .RequireAuthorization();

        group.MapGet("/type/{type}", async (IPokemonService service, string type) =>
            {
                var result = await service.GetByTypeAsync(type);
                return Results.Ok(result);
            }).WithName("GetPokemonByType")
            .RequireAuthorization();

        group.MapPost("/", async (
                IPokemonService service,
                IValidator<CreatePokemonDto> validator,
                CreatePokemonDto dto) =>
            {
                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                {
                    return Results.ValidationProblem(validation.ToDictionary());
                }

                var result = await service.CreateAsync(dto);
                return Results.Created($"/pokemon/{result.Id}", result);
            }).WithName("CreatePokemon")
            .RequireAuthorization();

        group.MapPut("/{id}", async (
                IPokemonService service,
                IValidator<UpdatePokemonDto> validator,
                int id,
                UpdatePokemonDto dto) =>
            {
                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                {
                    return Results.ValidationProblem(validation.ToDictionary());
                }

                var result = await service.UpdateAsync(id, dto);
                return result is null ? Results.NotFound($"Pokémon #{id} not found in the Pokédex!") : Results.Ok(result);
            }).WithName("UpdatePokemon")
            .RequireAuthorization();

        group.MapDelete("/{id}", async (
                IPokemonService service, int id) =>
            {
                var result = await service.DeleteAsync(id);
                return result ? Results.NoContent() : Results.NotFound($"Pokémon #{id} not found in the Pokédex!");
            }).WithName("DeletePokemon")
            .RequireAuthorization();
    }
}