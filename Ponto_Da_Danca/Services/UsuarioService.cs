// 5. Services/UsuarioService.cs
// Executa a regra de negócio orquestrando DTOs, Entidades e o Data.
using PontoDaDanca.Data;
using PontoDaDanca.DTOs;
using PontoDaDanca.Entities;

namespace PontoDaDanca.Services;

public class UsuarioService
{
    private readonly AppDbContext _context;

    public UsuarioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario> CriarUsuarioAsync(CriarUsuarioRequest request)
    {
        // Aqui entraria o código do Validator para checar regras
        // Aqui entraria a lógica de Infrastructure para encriptar a senha

        var novoUsuario = new Usuario(request.Nome, request.Email, request.Tipo);

        _context.Usuarios.Add(novoUsuario);
        await _context.SaveChangesAsync();

        return novoUsuario;
    }
}