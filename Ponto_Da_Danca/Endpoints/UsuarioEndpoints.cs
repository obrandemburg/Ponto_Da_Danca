// 6. Endpoints/UsuarioEndpoints.cs
// Isola as rotas Minimal API fora do Program.cs
using PontoDaDanca.DTOs;
using PontoDaDanca.Services;

namespace PontoDaDanca.Endpoints;

public static class UsuarioEndpoints
{
    public static void MapUsuarioEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/usuarios", async (CriarUsuarioRequest request, UsuarioService service) =>
        {
            var usuario = await service.CriarUsuarioAsync(request);
            return Results.Created($"/api/usuarios/{usuario.Id}", usuario);
        });
    }
}