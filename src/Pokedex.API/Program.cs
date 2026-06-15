using Pokedex.API;
using Pokedex.API.Endpoints;
using Pokedex.API.Extensions;
using Pokedex.Application;
using Pokedex.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services
    .AddApplication()
    .AddPresentation(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Pokédex API";
        options.Theme = ScalarTheme.DeepSpace; // 🌌 fitting for a Pokédex!
    });
    
    app.ApplyMigrations();
}

app.UseHttpsRedirection();
app.UseRequestContextLogging();

app.UseAuthentication();
app.UseAuthorization(); 

app.MapAuthEndpoints();
app.MapPokemonEndpoints();

app.Run();