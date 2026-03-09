namespace Ponto_Da_Danca.Entities;

public class Ritmo
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty; // [cite: 33]
    public string Descricao { get; private set; } = string.Empty; // História, o que é [cite: 35]
    public string Modalidade { get; private set; } = string.Empty; // Dança de salão ou solo [cite: 36]

    // EF Core constructor
    protected Ritmo() { }

    public Ritmo(string nome, string descricao, string modalidade)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Descricao = descricao;
        Modalidade = modalidade;
    }
}