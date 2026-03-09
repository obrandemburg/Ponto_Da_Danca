// DTOs/UsuarioDTOs.cs
using FluentValidation;

namespace Ponto_Da_Danca.DTOs;

public record CriarUsuarioRequest(string Nome, string Email, string Senha, string Tipo);

public record EditarPerfilRequest(string FotoUrl, string NomeSocial, string Biografia);

// Validador para criação de usuário
public class CriarUsuarioValidator : AbstractValidator<CriarUsuarioRequest>
{
    public CriarUsuarioValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("Formato de e-mail inválido.");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");

        // Valida se o tipo fornecido corresponde às entidades do sistema
        RuleFor(x => x.Tipo)
            .Must(tipo => new[] { "Aluno", "Professor", "Bolsista", "Gerente", "Recepção", "Líder" }.Contains(tipo))
            .WithMessage("Tipo de usuário inválido. Deve ser Aluno, Professor, Bolsista, Gerente, Recepção ou Líder.");
    }
}

// Validador para edição de perfil
public class EditarPerfilValidator : AbstractValidator<EditarPerfilRequest>
{
    public EditarPerfilValidator()
    {
        RuleFor(x => x.NomeSocial)
            .MaximumLength(100).WithMessage("O nome social não pode exceder 100 caracteres.");

        RuleFor(x => x.Biografia)
            .MaximumLength(500).WithMessage("A biografia não pode exceder 500 caracteres.");
    }
}