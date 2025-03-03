namespace Application.Interfaces;

using Application.DTOs.Login;
using FluentValidation.Results;

/// <summary>
/// Interfaz para el servicio de validación de inicio de sesión.
/// </summary>
public interface ILoginValidator
{
    ValidationResult Validar(LoginDto loginDto);
}
