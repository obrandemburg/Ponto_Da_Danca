// 1. Entities/Usuario.cs
// Entidade pura, sem anotações de banco de dados.
namespace PontoDaDanca.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Tipo { get; private set; } = string.Empty; // Ex: Aluno, Bolsista, Recepcao

    public Usuario(string nome, string email, string tipo)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        Tipo = tipo;
    }
}