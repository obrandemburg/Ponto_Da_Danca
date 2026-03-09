// 5. Services/UsuarioService.cs
// Executa a regra de negócio orquestrando DTOs, Entidades e o Data.
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
        // Aqui entraria o código do Validator para checar regras

        // Como é um MVP, estamos passando a senha direto para o SenhaHash.
        // Em um cenário de produção, aqui entraria a lógica de Infrastructure 
        // para encriptar a senha (ex: BCrypt.Net.BCrypt.HashPassword(request.Senha))

        // Passando os 4 parâmetros exigidos pelo novo construtor:
        var novoUsuario = new Usuario(
            request.Nome,
            request.Email,
            request.Senha, // Adicionando a senha aqui!
            request.Tipo
        );

        _context.Usuarios.Add(novoUsuario);
        await _context.SaveChangesAsync();

        return novoUsuario;
    }
}