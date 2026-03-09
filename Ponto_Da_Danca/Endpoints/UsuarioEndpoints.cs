// Endpoints/UsuarioEndpoints.cs
using Ponto_Da_Danca.DTOs;
using Ponto_Da_Danca.Services;
using Ponto_Da_Danca.Infra;

namespace Ponto_Da_Danca.Endpoints;

public static class UsuarioEndpoints
{
    public static void MapUsuarioEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/usuarios");

        // Rota de criação com validação do FluentValidation aplicada via AddEndpointFilter
        group.MapPost("/", async (CriarUsuarioRequest request, UsuarioService service) =>
        {
            var usuario = await service.CriarUsuarioAsync(request);
            return Results.Created($"/api/usuarios/{usuario.Id}", usuario);
        })
        .AddEndpointFilter<ValidationFilter<CriarUsuarioRequest>>(); // <--- Filtro de Validação

        // Endpoint: Editar Perfil
        // Requisito: Alunos, Bolsistas, Professores, Gerentes, Líder devem ter a opção para editar perfil
        group.MapPut("/{id}/perfil", async (Guid id, EditarPerfilRequest request, UsuarioService service) =>
        {
            try
            {
                await service.AtualizarPerfilAsync(id, request);
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.NotFound(new { Message = ex.Message });
            }
        })
        .AddEndpointFilter<ValidationFilter<EditarPerfilRequest>>();

        // Endpoint: Inserção em Massa (Bulk Insert)
        group.MapPost("/bulk", async (List<CriarUsuarioRequest> requests, UsuarioService service) =>
        {
            await service.InserirUsuariosEmMassaAsync(requests);
            return Results.Ok(new { Message = $"{requests.Count} usuários importados com sucesso." });
        });
    }
}