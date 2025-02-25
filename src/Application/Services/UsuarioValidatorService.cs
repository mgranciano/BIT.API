namespace Infrastructure.Services;

using Application.DTOs;
using Application.Interfaces;
using FluentValidation;
using FluentValidation.Results;

/// <summary>
/// Servicio para validar datos de usuarios utilizando FluentValidation.
/// </summary>
public class UsuarioValidatorService : AbstractValidator<UsuarioDto>, IUsuarioValidator
{
    public UsuarioValidatorService()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es obligatorio.")
            .EmailAddress().WithMessage("El formato del email no es válido.");

        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.");

        RuleFor(x => x.UsuarioId)
            .NotEmpty().WithMessage("El ID de usuario es obligatorio.");
    }

    /// <summary>
    /// Método para validar un usuario utilizando las reglas definidas.
    /// </summary>
    public ValidationResult Validar(UsuarioDto usuario)
    {
        return Validate(usuario);
    }
}
