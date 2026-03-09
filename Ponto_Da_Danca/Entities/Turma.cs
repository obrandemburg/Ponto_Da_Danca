namespace Ponto_Da_Danca.Entities;

public class Turma
{
    public Guid Id { get; private set; }
    public Guid RitmoId { get; private set; } // Chave Estrangeira
    public Ritmo Ritmo { get; private set; } = null!; // [cite: 38]
    public Guid ProfessorId { get; private set; } // Relaciona com a Entidade Usuario [cite: 42]
    public string Horario { get; private set; } = string.Empty; // [cite: 39]
    public string DiaFixoSemana { get; private set; } = string.Empty; // [cite: 40]
    public string Sala { get; private set; } = string.Empty; // [cite: 41]
    public int LimiteAlunos { get; private set; } // [cite: 43]
    public string NivelTecnico { get; private set; } = string.Empty; // básico, intermediário, avançado [cite: 45]
    public DateTime DataInicio { get; private set; } // [cite: 46]

    protected Turma() { }

    public Turma(Guid ritmoId, Guid professorId, string horario, string diaFixoSemana, string sala, int limiteAlunos, string nivelTecnico, DateTime dataInicio)
    {
        Id = Guid.NewGuid();
        RitmoId = ritmoId;
        ProfessorId = professorId;
        Horario = horario;
        DiaFixoSemana = diaFixoSemana;
        Sala = sala;
        LimiteAlunos = limiteAlunos;
        NivelTecnico = nivelTecnico;
        DataInicio = dataInicio;
    }
}