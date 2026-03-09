using Ponto_Da_Danca.Data;
using Ponto_Da_Danca.DTOs;
using Ponto_Da_Danca.Entities;
using BCrypt.Net;

namespace Ponto_Da_Danca.Services;

public class UsuarioService
{
    private readonly AppDbContext _context;

    public UsuarioService(AppDbContext context)
    {
        _context = context;
    }

    // Método para criar qualquer tipo de usuário com Hash de Senha
    public async Task<Usuario> CriarUsuarioAsync(CriarUsuarioRequest request)
    {
        string senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha);

        Usuario usuario = request.Tipo switch
        {
            "Aluno" => new Aluno(request.Nome, request.Email, senhaHash),
            "Professor" => new Professor(request.Nome, request.Email, senhaHash),
            "Bolsista" => new Bolsista(request.Nome, request.Email, senhaHash),
            "Gerente" => new Gerente(request.Nome, request.Email, senhaHash),
            "Recepção" => new Recepcao(request.Nome, request.Email, senhaHash),
            "Líder" => new Lider(request.Nome, request.Email, senhaHash),
            _ => throw new ArgumentException("Tipo de usuário inválido")
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    // Método que estava faltando para atualizar o perfil
    public async Task AtualizarPerfilAsync(Guid id, EditarPerfilRequest request)
    {
        var usuario = await _context.Usuarios.FindAsync(id)
            ?? throw new Exception("Usuário não encontrado.");

        usuario.EditarPerfil(request.FotoUrl, request.NomeSocial, request.Biografia);

        await _context.SaveChangesAsync();
    }
}