#nullable disable
using Dominio.Entities;

namespace Application.DTOs.Login;

/// <summary>
/// Representa la respuesta de autenticación con JWT.
/// </summary>
public class LoginResponseDto
{
    /// <summary>
    /// Token de autenticación.
    /// </summary>
    public string Token { get; set; }
    /// <summary>
    /// Modulos a los que tiene acceso el usuario.
    /// </summary>
    public List<ModuloDto> Modulos { get; set; }
    /// <summary>
    /// Perfil del usuario.
    /// </summary>
    public DatosAccesoUsuarioDto Perfil { get; set; }
}