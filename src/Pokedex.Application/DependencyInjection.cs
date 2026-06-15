using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Pokedex.Application.Interfaces;
using Pokedex.Application.Mappings;
using Pokedex.Application.Services;

namespace Pokedex.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(
            typeof(DependencyInjection).Assembly,
            includeInternalTypes: true);

        services.AddScoped<PokemonMapper>();
        
        services.AddScoped<IPokemonService, PokemonService>();
        
        return services;
    }
}