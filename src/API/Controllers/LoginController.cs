namespace API.Controllers;

using Application.DTOs;
using Application.UseCases.Usuarios.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

/// <summary>
/// Controlador para la autenticación de usuarios.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly JwtTokenService _jwtTokenService;
    private readonly IMediator _mediator; // ✅ Usamos MediatR en lugar de `IUsuarioRepository`
    private readonly ILogger<LoginController> _logger;

    public LoginController(JwtTokenService jwtTokenService, IMediator mediator, ILogger<LoginController> logger)
    {
        _jwtTokenService = jwtTokenService;
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Inicia sesión y devuelve un token JWT si las credenciales son válidas.
    /// </summary>
    /// <param name="loginDto">Datos de inicio de sesión.</param>
    [HttpPost("authenticate")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
    {
        _logger.LogInformation("📢 Intento de inicio de sesión para {Email}", loginDto.Email);

        // 🔹 Buscar al usuario por Email usando `MediatR`
        var usuario = await _mediator.Send(new ObtenerUsuarioPorEmailQuery(loginDto.Email));

        // 🔹 Validar si el usuario existe y tiene `estatus = true`
        if (usuario == null || !usuario.Estatus)
        {
            _logger.LogWarning("⚠️ Usuario no encontrado o inactivo: {Email}", loginDto.Email);
            return Unauthorized(ResponseDto<object>.Error("Usuario no encontrado o inactivo."));
        }

        // 🔹 Si el usuario existe y está activo, generar JWT
        var token = _jwtTokenService.GenerateToken(usuario.UsuarioId, usuario.Email);
        _logger.LogInformation("✅ Inicio de sesión exitoso para {Email}", loginDto.Email);

        var response = new LoginResponseDto { Token = token };
        return Ok(ResponseDto<LoginResponseDto>.Exito("Inicio de sesión exitoso.", response));
    }
}
