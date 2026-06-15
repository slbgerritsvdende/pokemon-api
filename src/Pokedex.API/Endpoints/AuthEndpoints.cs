using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Pokedex.Application.DTOs;
using Pokedex.Application.Interfaces;

namespace Pokedex.API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth").WithTags("Auth");
        
        group.MapPost("/register", async (
            RegisterDto dto,
            IValidator<RegisterDto> validator,
            UserManager<IdentityUser> userManager,
            ITokenService tokenService) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.ValidationProblem(validation.ToDictionary());

            var user = new IdentityUser
            {
                UserName = dto.Username,
                Email = dto.Email
            };

            var result = await userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return Results.BadRequest(result.Errors.Select(e => e.Description));

            return Results.Ok(tokenService.GenerateToken(user));
        });

        group.MapPost("/login", async (
            LoginDto dto,
            IValidator<LoginDto> validator,
            UserManager<IdentityUser> userManager,
            ITokenService tokenService) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.ValidationProblem(validation.ToDictionary());

            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user is null || !await userManager.CheckPasswordAsync(user, dto.Password))
                return Results.Unauthorized();

            return Results.Ok(tokenService.GenerateToken(user));
        });     
    }
}