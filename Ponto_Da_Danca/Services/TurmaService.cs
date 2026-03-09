using Microsoft.EntityFrameworkCore;
using Ponto_Da_Danca.Data;
using Ponto_Da_Danca.DTOs;
using Ponto_Da_Danca.Entities;

namespace Ponto_Da_Danca.Services;

public class TurmaService
{
    private readonly AppDbContext _context;

    public TurmaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TurmaResponse> CriarTurmaAsync(CriarTurmaRequest request)
    {
        var novaTurma = new Turma(request.RitmoId, request.ProfessorId, request.Horario, request.DiaFixoSemana, request.Sala, request.LimiteAlunos, request.NivelTecnico, request.DataInicio);

        _context.Turmas.Add(novaTurma);
        await _context.SaveChangesAsync();

        // Busca o ritmo para retornar o nome no DTO
        var ritmo = await _context.Ritmos.FindAsync(request.RitmoId);

        return new TurmaResponse(novaTurma.Id, ritmo?.Nome ?? "", novaTurma.Horario, novaTurma.DiaFixoSemana, novaTurma.Sala, novaTurma.LimiteAlunos, novaTurma.NivelTecnico);
    }

    [cite_start]// Funcionalidade: Listar turmas da escola com filtros [cite: 95]
    public async Task<List<TurmaResponse>> ListarTurmasAsync(string? modalidade, string? nivel, string? diaSemana)
    {
        var query = _context.Turmas.Include(t => t.Ritmo).AsQueryable();

        [cite_start]// Aplicando os filtros descritos na abstração [cite: 96, 100, 101]
        if (!string.IsNullOrWhiteSpace(modalidade))
            query = query.Where(t => t.Ritmo.Modalidade == modalidade);

        if (!string.IsNullOrWhiteSpace(nivel))
            query = query.Where(t => t.NivelTecnico == nivel);

        if (!string.IsNullOrWhiteSpace(diaSemana))
            query = query.Where(t => t.DiaFixoSemana == diaSemana);

        var turmas = await query.ToListAsync();

        return turmas.Select(t => new TurmaResponse(
            t.Id, t.Ritmo.Nome, t.Horario, t.DiaFixoSemana, t.Sala, t.LimiteAlunos, t.NivelTecnico
        )).ToList();
    }
}