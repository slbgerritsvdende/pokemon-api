# 🎮 Pokédex REST API

> *"This Pokédex... records data on all the Pokémon in the world. You must bring it to completion."* — Professor Oak

A clean-architecture ASP.NET Core REST API for managing Pokémon data. Built to teach .NET best practices.

---

## 🗂️ Project Structure

```
PokedexAPI/
├── src/
│   ├── Pokedex.Domain/               # Enterprise business rules
│   │   ├── Entities/
│   │   │   ├── Pokemon.cs
│   │   │   └── PokemonType.cs
│   │   └── Interfaces/
│   │       └── IPokemonRepository.cs
│   │
│   ├── Pokedex.Application/          # Application business rules
│   │   ├── DTOs/
│   │   │   └── PokemonDtos.cs
│   │   ├── Interfaces/
│   │   │   └── IPokemonService.cs
│   │   └── Services/
│   │       └── PokemonService.cs
│   │
│   ├── Pokedex.Infrastructure/       # DB, external concerns
│   │   ├── Data/
│   │   │   └── PokedexDbContext.cs
│   │   └── Repositories/
│   │       └── PokemonRepository.cs
│   │
│   └── Pokedex.API/                  # Entry point
│       ├── Controllers/
│       │   └── PokemonController.cs
│       └── Program.cs
│
└── tests/
    └── Pokedex.Tests/
        └── Services/
            └── PokemonServiceTests.cs
```

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- Your favourite IDE (Visual Studio / Rider / VS Code)

### Run the API

```bash
cd src/Pokedex.API
dotnet run
```

Open [https://localhost:5001/swagger](https://localhost:5001/swagger) to explore the endpoints.

### Run the Tests

```bash
cd tests/Pokedex.Tests
dotnet test
```

---

## 📡 Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/pokemon` | Get all Pokémon (paginated) |
| GET | `/api/pokemon/{id}` | Get one Pokémon |
| GET | `/api/pokemon/type/{type}` | Filter by type |
| POST | `/api/pokemon` | Add a new Pokémon |
| PUT | `/api/pokemon/{id}` | Update a Pokémon |
| DELETE | `/api/pokemon/{id}` | Release a Pokémon 😢 |

---

## 🧭 Next Steps (Your Learning Path)

- [x] Add **FluentValidation** for request validation
- [x] Replace manual mapping with **Mapperly**
- [x] Add **global error handling middleware**
- [ ] Add **authentication** with ASP.NET Identity
- [x] Swap SQLite for **SQL Server** / **PostgreSQL**
- [ ~~~~] Add **Redis caching** for popular Pokémon lookups
- [ ] Write **integration tests** with WebApplicationFactory

---

*A Pokédex is never truly complete. Neither is a codebase. Keep improving, young trainer!*
