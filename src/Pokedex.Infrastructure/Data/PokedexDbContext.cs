using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pokedex.Domain.Entities;

namespace Pokedex.Infrastructure.Data;

public class PokedexDbContext(DbContextOptions<PokedexDbContext> options) : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<Pokemon> Pokemon => Set<Pokemon>();
    public DbSet<PokemonType> PokemonTypes => Set<PokemonType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pokemon>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Description).HasMaxLength(500);
            entity.HasMany(p => p.Types)
                  .WithOne(t => t.Pokemon)
                  .HasForeignKey(t => t.PokemonId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PokemonType>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.TypeName).IsRequired().HasMaxLength(50);
        });

        // Seed some starter data — every Pokédex needs its starters!
        modelBuilder.Entity<Pokemon>().HasData(
            new Pokemon { Id = 1, PokedexNumber = 1, Name = "Bulbasaur", Description = "A strange seed was planted on its back at birth.", HeightInMeters = 0.7, WeightInKg = 6.9 },
            new Pokemon { Id = 2, PokedexNumber = 4, Name = "Charmander", Description = "The flame at the tip of its tail makes a sound as it burns.", HeightInMeters = 0.6, WeightInKg = 8.5 },
            new Pokemon { Id = 3, PokedexNumber = 7, Name = "Squirtle", Description = "After birth, its back swells and hardens into a shell.", HeightInMeters = 0.5, WeightInKg = 9.0 }
        );

        modelBuilder.Entity<PokemonType>().HasData(
            new PokemonType { Id = 1, PokemonId = 1, TypeName = "Grass" },
            new PokemonType { Id = 2, PokemonId = 1, TypeName = "Poison" },
            new PokemonType { Id = 3, PokemonId = 2, TypeName = "Fire" },
            new PokemonType { Id = 4, PokemonId = 3, TypeName = "Water" }
        );
    }
}
