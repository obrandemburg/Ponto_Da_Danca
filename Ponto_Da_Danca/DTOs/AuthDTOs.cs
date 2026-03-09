namespace Ponto_Da_Danca.DTOs;

public record LoginRequest(string Email, string Senha);
public record LoginResponse(string Token, string Nome, string Tipo);