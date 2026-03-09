namespace Ponto_Da_Danca.DTOs;

public record CriarTurmaRequest(Guid RitmoId, Guid ProfessorId, string Horario, string DiaFixoSemana, string Sala, int LimiteAlunos, string NivelTecnico, DateTime DataInicio);

public record TurmaResponse(Guid Id, string NomeRitmo, string Horario, string DiaFixoSemana, string Sala, int LimiteAlunos, string NivelTecnico);