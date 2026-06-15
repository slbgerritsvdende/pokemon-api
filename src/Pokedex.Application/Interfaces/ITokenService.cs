using Microsoft.AspNetCore.Identity;
using Pokedex.Application.DTOs;

namespace Pokedex.Application.Interfaces;

public interface ITokenService
{
    AuthResponseDto GenerateToken(IdentityUser user);
}