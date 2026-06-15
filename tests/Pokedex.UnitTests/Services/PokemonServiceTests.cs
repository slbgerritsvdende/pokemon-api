using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using Pokedex.Application.DTOs;
using Pokedex.Application.Mappings;
using Pokedex.Application.Services;
using Pokedex.Domain.Entities;
using Pokedex.Domain.Interfaces;

namespace Pokedex.UnitTests.Services;

public class PokemonServiceTests
{
    private readonly PokemonService _service;
    private readonly PokemonMapper _mapper;
    private readonly IMemoryCache _cache;
    
    private readonly IPokemonRepository _repoMock;

    public PokemonServiceTests()
    {
        _repoMock = Substitute.For<IPokemonRepository>();
        _mapper = new PokemonMapper();   
        _cache = new MemoryCache(new MemoryCacheOptions());
        _service = new PokemonService(_repoMock, _mapper, _cache);
    }
    
        [Fact]
    public async Task GetByIdAsync_WhenPokemonExists_ReturnsPokemonDto()
    {
        // Arrange — prepare our test data
        var pikachu = new Pokemon
        {
            Id = 25,
            PokedexNumber = 25,
            Name = "Pikachu",
            Description = "When several of these Pokémon gather, their electricity can cause lightning storms.",
            HeightInMeters = 0.4,
            WeightInKg = 6.0,
            Types = new List<PokemonType> { new() { TypeName = "Electric" } }
        };

        _repoMock.GetByIdAsync(25).Returns(pikachu);

        // Act — call the method we're testing
        var result = await _service.GetByIdAsync(25);

        // Assert — verify the result
        Assert.NotNull(result);
        Assert.Equal("Pikachu", result.Name);
        Assert.Contains("Electric", result.Types);
    }

    [Fact]
    public async Task GetByIdAsync_WhenPokemonDoesNotExist_ReturnsNull()
    {
        // Arrange
        _repoMock.GetByIdAsync(999).Returns((Pokemon?)null);

        // Act
        var result = await _service.GetByIdAsync(999);

        // Assert — that Pokémon doesn't exist in our Pokédex!
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ValidDto_ReturnsMappedPokemonDto()
    {
        // Arrange
        var dto = new CreatePokemonDto(
            PokedexNumber: 133,
            Name: "Eevee",
            Description: "Its genetic code is irregular. It may mutate if it is exposed to radiation from element stones.",
            HeightInMeters: 0.3,
            WeightInKg: 6.5,
            Types: new[] { "Normal" }
        );

        _repoMock.AddAsync(Arg.Any<Pokemon>())
                 .Returns(callInfo => { var p = callInfo.Arg<Pokemon>(); p.Id = 1; return p; });

        // Act
        var result = await _service.CreateAsync(dto);

        // Assert
        Assert.Equal("Eevee", result.Name);
        Assert.Contains("Normal", result.Types);
        await _repoMock.Received(1).AddAsync(Arg.Any<Pokemon>());
    }
}