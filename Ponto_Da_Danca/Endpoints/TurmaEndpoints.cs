using Ponto_Da_Danca.DTOs;
using Ponto_Da_Danca.Services;

namespace Ponto_Da_Danca.Endpoints;

public static class TurmaEndpoints
{
    public static void MapTurmaEndpoints(this IEndpointRouteBuilder app)
    {
        var grupo = app.MapGroup("/api/turmas");

        // Listar turmas da escola [cite: 95]
        grupo.MapGet("/", async (string? modalidade, string? nivel, string? diaSemana, TurmaService service) =>
        {
            var turmas = await service.ListarTurmasAsync(modalidade, nivel, diaSemana);
            return Results.Ok(turmas);
        });

        // Recepção pode criar novas turmas [cite: 72]
        grupo.MapPost("/", async (CriarTurmaRequest request, TurmaService service) =>
        {
            var turma = await service.CriarTurmaAsync(request);
            return Results.Created($"/api/turmas/{turma.Id}", turma);
        });
    }
}