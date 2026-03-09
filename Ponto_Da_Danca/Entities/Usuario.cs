// Entities/Usuario.cs
namespace Ponto_Da_Danca.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string SenhaHash { get; private set; } = string.Empty;
    public string Tipo { get; private set; } = string.Empty;

    // Novas propriedades adicionadas para edição de perfil
    public string NomeSocial { get; private set; } = string.Empty;
    public string Biografia { get; private set; } = string.Empty;
    public string FotoUrl { get; private set; } = string.Empty;

    protected Usuario() { }

    public Usuario(string nome, string email, string senhaHash, string tipo)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        Tipo = tipo;
    }

    // Método de domínio atualizado para as regras de negócio de perfil
    public void EditarPerfil(string fotoUrl, string nomeSocial, string biografia)
    {
        FotoUrl = fotoUrl;
        NomeSocial = nomeSocial;
        Biografia = biografia;
    }
}