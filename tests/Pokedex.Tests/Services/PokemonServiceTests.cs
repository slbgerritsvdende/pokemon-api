using Moq;
using Pokedex.Application.DTOs;
using Pokedex.Application.Services;
using Pokedex.Domain.Entities;
using Pokedex.Domain.Interfaces;

namespace Pokedex.Tests.Services;

public class PokemonServiceTests
{
    private readonly Mock<IPokemonRepository> _repoMock;
    private readonly PokemonService _service;

    public PokemonServiceTests()
    {
        _repoMock = new Mock<IPokemonRepository>();
        _service = new PokemonService(_repoMock.Object);
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

        _repoMock.Setup(r => r.GetByIdAsync(25)).ReturnsAsync(pikachu);

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
        _repoMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Pokemon?)null);

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

        _repoMock.Setup(r => r.AddAsync(It.IsAny<Pokemon>()))
                 .ReturnsAsync((Pokemon p) => { p.Id = 1; return p; });

        // Act
        var result = await _service.CreateAsync(dto);

        // Assert
        Assert.Equal("Eevee", result.Name);
        Assert.Contains("Normal", result.Types);
        _repoMock.Verify(r => r.AddAsync(It.IsAny<Pokemon>()), Times.Once);
    }
}
