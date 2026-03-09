// 2. DTOs/CriarUsuarioRequest.cs
// Apenas carrega os dados da requisição HTTP.
namespace PontoDaDanca.DTOs;

public record CriarUsuarioRequest(string Nome, string Email, string Senha, string Tipo);