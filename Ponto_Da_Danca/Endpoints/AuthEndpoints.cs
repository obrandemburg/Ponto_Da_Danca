using Microsoft.EntityFrameworkCore;
using Ponto_Da_Danca.Data;
using Ponto_Da_Danca.DTOs;
using Ponto_Da_Danca.Services;

namespace Ponto_Da_Danca.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth").WithTags("Autenticação");

        group.MapPost("/login", async (LoginRequest request, AppDbContext db, TokenService tokenService) =>
        {
            var usuario = await db.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);

            // Validação usando o BCrypt.Verify
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
                return Results.Unauthorized();

            var token = tokenService.GerarToken(usuario);

            return Results.Ok(new LoginResponse(token, usuario.Nome, usuario.Tipo));
        })
        .Produces<LoginResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized);
    }
}