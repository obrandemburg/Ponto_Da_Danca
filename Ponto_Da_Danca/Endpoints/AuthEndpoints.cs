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

            // Em produção, utilize BCrypt para validar o Hash! 
            // Para o MVP inicial validaremos de forma direta se a senha bater com o hash salvo.
            if (usuario == null || usuario.SenhaHash != request.Senha)
                return Results.Unauthorized();

            var token = tokenService.GerarToken(usuario);

            return Results.Ok(new LoginResponse(token, usuario.Nome, usuario.Tipo));
        })
        .Produces<LoginResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized);
    }
}