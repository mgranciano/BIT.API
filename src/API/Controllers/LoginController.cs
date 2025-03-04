namespace API.Controllers;

using Application.DTOs.Login;
using Application.DTOs;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Extensions;
using Dominio.Entities;

/// <summary>
/// Controlador para la autenticación de usuarios.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly JwtTokenService _jwtTokenService;
    private readonly ILogService _logService;
    private readonly ILoginValidator _loginValidator;
    private readonly IUsuarioService _usuarioService;

    public LoginController(JwtTokenService jwtTokenService,
                           ILogService logService,
                           ILoginValidator loginValidator,
                           IUsuarioService usuarioService)
    {
        _jwtTokenService = jwtTokenService;
        _logService = logService;
        _loginValidator = loginValidator;
        _usuarioService = usuarioService;
    }

    /// <summary>
    /// Inicia sesión y devuelve un token JWT si las credenciales son válidas.
    /// </summary>
    /// <param name="loginDto">Datos de inicio de sesión.</param>
    [HttpPost("authenticate")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
    {
        _logService.Informacion(nameof(UsuarioController), $"Intento de inicio de sesión para: {loginDto.Email}");
        LoginResponseDto respDto = new LoginResponseDto();
        var validationResult = _loginValidator.Validar(loginDto);
        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            _logService.Advertencia(nameof(LoginController), $"Errores de validación: {string.Join(", ", errores)}");
            return BadRequest(ResponseDto<object>.Error("Datos inválidos", errores));
        }
        var usuario = await _usuarioService.ObtenerUsuarioPorEmailAsync(loginDto.Email);

        if (usuario != null && usuario.Estado)
        {
            respDto.Perfil = usuario.ToDatosAccesoUsuario();
            respDto.Token = _jwtTokenService.GenerateToken(usuario.IdUsuario, usuario.CorreoElectronico);
            _logService.Correcto(nameof(UsuarioController), $"Inicio de sesión exitoso para : {loginDto.Email}");

            if (!string.IsNullOrEmpty(respDto.Token))
            {
                var Modulos = await _usuarioService.ObtenerModulosPorUsuarioAsync(usuario.IdUsuario);
                if (Modulos != null && Modulos.Any())
                {
                    respDto.Modulos = Modulos.ToModuloGeneralDtoList();
                    _logService.Correcto(nameof(UsuarioController), $"Modulos obtenidos correctamente para: {loginDto.Email}");
                }
                else
                {
                    _logService.Advertencia(nameof(UsuarioController), $"No se encontraron modulos para: {loginDto.Email}");
                }

                return Ok(ResponseDto<LoginResponseDto>.Exito("Inicio de sesión exitoso.", respDto));
            }
            else
            {
                _logService.Error(nameof(UsuarioController), $"Error al generar el token para: {loginDto.Email}");
                return StatusCode(500, ResponseDto<object>.Error("Error al generar el token."));
            }
        }
        else
        {
            _logService.Error(nameof(UsuarioController), $"Usuario no encontrado o inactivo: {loginDto.Email}");
            return Unauthorized(ResponseDto<object>.Error("Usuario no encontrado o inactivo."));
        }


    }
}
