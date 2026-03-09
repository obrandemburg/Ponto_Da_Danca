// 2. DTOs/CriarUsuarioRequest.cs
// Apenas carrega os dados da requisição HTTP.
namespace Ponto_Da_Danca.DTOs;

public record CriarUsuarioRequest(string Nome, string Email, string Senha, string Tipo);