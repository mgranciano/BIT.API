namespace Infrastructure.Services;

using Application.DTOs.Login;
using Application.Interfaces;
using FluentValidation;
using FluentValidation.Results;

/// <summary>
/// Servicio para validar datos de inicio de sesión utilizando FluentValidation.
/// </summary>
public class LoginValidatorService : AbstractValidator<LoginDto>, ILoginValidator
{
    public LoginValidatorService()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es obligatorio.")
            .EmailAddress().WithMessage("El formato del email no es válido.");
    }

    /// <summary>
    /// Método para validar un login utilizando las reglas definidas.
    /// </summary>
    public ValidationResult Validar(LoginDto loginDto)
    {
        return Validate(loginDto);
    }
}
