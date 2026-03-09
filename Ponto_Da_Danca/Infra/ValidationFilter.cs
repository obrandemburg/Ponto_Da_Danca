// Infra/ValidationFilter.cs
using FluentValidation;

namespace Ponto_Da_Danca.Infra;

// Filtro genérico que intercepta a requisição e executa o FluentValidation
public class ValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        // Busca o validador registrado no container de injeção de dependência
        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

        if (validator is not null)
        {
            // Pega o objeto do corpo da requisição
            var entity = context.Arguments.OfType<T>().FirstOrDefault();
            if (entity is not null)
            {
                var validationResult = await validator.ValidateAsync(entity);
                if (!validationResult.IsValid)
                {
                    // Retorna 400 Bad Request com os erros de validação
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
            }
        }
        return await next(context);
    }
}