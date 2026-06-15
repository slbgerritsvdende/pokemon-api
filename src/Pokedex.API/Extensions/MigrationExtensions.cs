using Microsoft.EntityFrameworkCore;
using Pokedex.Infrastructure.Data;

namespace Pokedex.API.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext =
            scope.ServiceProvider.GetRequiredService<PokedexDbContext>();

        dbContext.Database.Migrate();
    }
}