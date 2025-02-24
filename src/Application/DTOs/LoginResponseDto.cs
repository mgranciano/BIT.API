#nullable disable
namespace Application.DTOs;

/// <summary>
/// Representa la respuesta de autenticación con JWT.
/// </summary>
public class LoginResponseDto
{
    public string Token { get; set; }
}