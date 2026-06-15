using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Pokedex.Application.Interfaces;
using Pokedex.Domain.Interfaces;
using Pokedex.Infrastructure.Data;
using Pokedex.Infrastructure.Repositories;
using Pokedex.Infrastructure.Services;

namespace Pokedex.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        
        services.AddSingleton(_ =>
            new DbConnectionFactory(new NpgsqlDataSourceBuilder(connectionString).Build()));

        services.AddDbContext<PokedexDbContext>(options => options
            .UseNpgsql(connectionString));
        
        services.AddScoped<IPokemonRepository, PokemonRepository>();
        services.AddScoped<ITokenService, TokenService>();
    }
}