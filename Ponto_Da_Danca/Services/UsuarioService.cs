// Services/UsuarioService.cs
using EFCore.BulkExtensions;
using Ponto_Da_Danca.Data;
using Ponto_Da_Danca.DTOs;
using Ponto_Da_Danca.Entities;

namespace Ponto_Da_Danca.Services;

public class UsuarioService
{
    private readonly AppDbContext _context;

    public UsuarioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario> CriarUsuarioAsync(CriarUsuarioRequest request)
    {
        // Hash de senha fictício para exemplo
        var usuario = new Usuario(request.Nome, request.Email, "hash_senha", request.Tipo);
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task InserirUsuariosEmMassaAsync(IEnumerable<CriarUsuarioRequest> requests)
    {
        // Converte a lista de DTOs para Entidades
        var usuarios = requests.Select(r =>
            new Usuario(r.Nome, r.Email, "hash_padrao", r.Tipo)).ToList();

        // Utiliza o EFCore.BulkExtensions para inserir milhares de registros em segundos
        // Esta operação gera comandos COPY/INSERT em lote no PostgreSQL, ignorando o tracker do EF para performance
        await _context.BulkInsertAsync(usuarios);
    }

    public async Task AtualizarPerfilAsync(Guid id, EditarPerfilRequest request)
    {
        var usuario = await _context.Usuarios.FindAsync(id)
            ?? throw new Exception("Usuário não encontrado.");

        usuario.EditarPerfil(request.FotoUrl, request.NomeSocial, request.Biografia);
        await _context.SaveChangesAsync();
    }
}