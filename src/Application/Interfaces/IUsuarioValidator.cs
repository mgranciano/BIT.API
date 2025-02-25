namespace Application.Interfaces;

using Application.DTOs;
using FluentValidation.Results;

/// <summary>
/// Interfaz para el servicio de validación de usuarios.
/// </summary>
public interface IUsuarioValidator
{
    ValidationResult Validar(UsuarioDto usuario);
}