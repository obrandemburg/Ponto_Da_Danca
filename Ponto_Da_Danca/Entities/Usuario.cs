namespace Ponto_Da_Danca.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string SenhaHash { get; private set; } = string.Empty; // Necessário para login
    public string Tipo { get; private set; } = string.Empty; // Aluno, Professor, Bolsista, Gerente, Recepcao, Lider

    protected Usuario() { } // Para o EF Core

    public Usuario(string nome, string email, string senhaHash, string tipo)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        Tipo = tipo;
    }

    public void AtualizarPerfil(string nome)
    {
        Nome = nome;
    }
}