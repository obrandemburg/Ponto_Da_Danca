using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Ponto_Da_Danca.Data;
using Ponto_Da_Danca.DTOs;
using Ponto_Da_Danca.Services;
using System.Security.Claims;

namespace Ponto_Da_Danca.Endpoints;

public static class UsuarioEndpoints
{
    public static void MapUsuarioEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/usuarios").WithTags("Usuários").RequireAuthorization();

        // Recepção pode cadastrar alunos e professores [cite: 54, 56]
        group.MapPost("/cadastrar", async (CriarUsuarioRequest request, UsuarioService usuarioService) =>
        {
            var usuario = await usuarioService.CriarUsuarioAsync(request);
            return Results.Created($"/api/usuarios/{usuario.Id}", new { usuario.Id, usuario.Nome, usuario.Tipo });
        })
        .RequireAuthorization(policy => policy.RequireRole("Recepção", "Gerente", "Líder"));

        // Gerente pode adicionar bolsistas [cite: 62]
        group.MapPost("/bolsistas", async (CriarUsuarioRequest request, UsuarioService usuarioService) =>
        {
            if (request.Tipo != "Bolsista") return Results.BadRequest("Endpoint exclusivo para bolsistas.");
            var usuario = await usuarioService.CriarUsuarioAsync(request);
            return Results.Created($"/api/usuarios/{usuario.Id}", new { usuario.Id, usuario.Nome });
        })
        .RequireAuthorization(policy => policy.RequireRole("Gerente", "Líder"));

        // Editar próprio perfil (Qualquer usuário logado pode editar a si mesmo) [cite: 74]
        group.MapPut("/perfil", async (EditarPerfilRequest request, UsuarioService usuarioService, ClaimsPrincipal user) =>
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out Guid id)) return Results.Unauthorized();

            await usuarioService.AtualizarPerfilAsync(id, request);
            return Results.NoContent();
        });
    }
}