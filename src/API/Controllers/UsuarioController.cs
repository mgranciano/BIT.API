using API.Attributes;
using Microsoft.AspNetCore.Authorization;
using Application.Constants;
using Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Extensions;

namespace API.Controllers; 
/// <summary>
/// Controlador para la gestión de usuarios.
/// </summary>
/// <remarks>
/// Este controlador maneja la creación, actualización, eliminación y consulta de usuarios.
/// También incluye la integración con Serilog para el registro de logs.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsuarioController : ControllerBase
{
    private readonly ILogService _logService;
    private readonly IUsuarioValidator _usuarioValidator; 
    private readonly IUsuarioService _usuarioService;

    /// <summary>
    /// Constructor del controlador de usuarios.
    /// </summary>
    /// <param name="mediator">Instancia de MediatR para manejar las solicitudes CQRS.</param>
    /// <param name="logger">Instancia de Serilog para registrar eventos.</param>
    public UsuarioController(ILogService logService,
                             IUsuarioValidator usuarioValidator,
                             IUsuarioService usuarioService)
    {
        _logService = logService;
        _usuarioValidator = usuarioValidator;
        _usuarioService = usuarioService;
    }

    /// <summary>
    /// Obtiene la lista completa de usuarios registrados en el sistema.
    /// </summary>
    [HttpGet("obtenerUsuarios")]
    [ProducesUsuarioResponse(typeof(ResponseDto<IEnumerable<UsuarioDto>>), ApiDocumentacionUsuarios.ObtenerUsuariosSummary, ApiDocumentacionUsuarios.ObtenerUsuariosDescription)]
    public async Task<IActionResult> ObtenerUsuarios()
    {
        _logService.Informacion(nameof(UsuarioController), $"Se solicitó obtener todos los usuarios.");
        var usuarios = await _usuarioService.ObtenerUsuariosAsync();

        if (usuarios == null || !usuarios.Any())
        {
            _logService.Advertencia(nameof(UsuarioController), $"No se encontraron usuarios.");
            return NotFound(ResponseDto<object>.Advertencia(ApiMensajesUsuarios.UsuariosNoEncontrados));
        }

        _logService.Correcto(nameof(UsuarioController), $"Usuarios obtenidos correctamente.");
        return Ok(ResponseDto<IEnumerable<UsuarioDto>>.Exito(ApiMensajesUsuarios.UsuariosEncontrados, usuarios.ToDtoList()));
    }

    /// <summary>
    /// Obtiene un usuario por su identificador único.
    /// </summary>
    /// <param name="usuarioId">Identificador único del usuario.</param>
    [HttpGet("obtenerUsuario/{usuarioId}")]
    [ProducesUsuarioResponse(typeof(ResponseDto<UsuarioDto>), ApiDocumentacionUsuarios.ObtenerUsuarioSummary, ApiDocumentacionUsuarios.ObtenerUsuarioDescription)]
    public async Task<IActionResult> ObtenerUsuario([FromRoute] string usuarioId)
    {
        _logService.Informacion(nameof(UsuarioController), $"Se solicitó obtener el usuario con ID {usuarioId}.");

        var usuario = await _usuarioService.ObtenerUsuarioPorIdAsync(usuarioId);

        if (usuario == null)
        {
            _logService.Advertencia(nameof(UsuarioController), $"No se encontró el usuario con ID {usuarioId}.");
            return NotFound(ResponseDto<object>.Advertencia(ApiMensajesUsuarios.UsuarioNoEncontrado));
        }

        _logService.Correcto(nameof(UsuarioController), $"Usuario encontrado: {usuarioId}.");
        return Ok(ResponseDto<UsuarioDto>.Exito(ApiMensajesUsuarios.UsuarioEncontrado, usuario.ToDto()));
    }

    /// <summary>
    /// Registra un nuevo usuario en el sistema.
    /// </summary>
    /// <param name="usuario">Objeto con la información del usuario a registrar.</param>
    [HttpPost("altaUsuario")]
    [ProducesUsuarioResponse(typeof(ResponseDto<UsuarioDto>), ApiDocumentacionUsuarios.AltaUsuarioSummary, ApiDocumentacionUsuarios.AltaUsuarioDescription)]
    public async Task<IActionResult> AltaUsuario([FromBody] UsuarioDto usuario)
    {
        _logService.Informacion(nameof(UsuarioController), $"Se solicita el alta del usuario {usuario.Email}.");

        var validationResult = _usuarioValidator.Validar(usuario);

        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            _logService.Advertencia(nameof(UsuarioController), $"Errores de validación: {string.Join(", ", errores)}");
            return BadRequest(ResponseDto<object>.Error("Datos inválidos", errores));
        }

        _logService.Advertencia(nameof(UsuarioController), $"Se está registrando un nuevo usuario {usuario.Email}.");
        var nuevoUsuario = await _usuarioService.RegistrarUsuarioAsync(usuario.ToEntity()); 

        _logService.Correcto(nameof(UsuarioController), $"Usuario registrado correctamente: {nuevoUsuario.UsuarioId}.");
        return CreatedAtAction(nameof(ObtenerUsuario), new { usuarioId = nuevoUsuario.UsuarioId },
            ResponseDto<UsuarioDto>.Exito(ApiMensajesUsuarios.UsuarioCreado, nuevoUsuario.ToDto()));
    }

    /// <summary>
    /// Actualiza parcialmente un usuario existente.
    /// </summary>
    /// <param name="usuarioId">Identificador único del usuario a actualizar.</param>
    /// <param name="usuario">Objeto con los datos a modificar.</param>
    [HttpPatch("actualizarUsuario/{usuarioId}")]
    [ProducesUsuarioResponse(typeof(ResponseDto<UsuarioDto>), ApiDocumentacionUsuarios.ActualizarUsuarioSummary, ApiDocumentacionUsuarios.ActualizarUsuarioDescription)]
    public async Task<IActionResult> ActualizarUsuario([FromRoute] string usuarioId, [FromBody] UsuarioDto usuario)
    {
        _logService.Informacion(nameof(UsuarioController), $"Se solicita la actualización del usuario {usuario.Email}.");

        if (usuarioId != usuario.UsuarioId)
        {
            _logService.Advertencia(nameof(UsuarioController), $"ID de usuario en URL no coincide con el cuerpo de la solicitud.");
            return BadRequest(ResponseDto<object>.Error(ApiMensajesUsuarios.IdNoCoincide));
        }

        _logService.Advertencia(nameof(UsuarioController), $"Se está actualizando el usuario con ID {usuarioId}.");
        var usuarioActualizado = await _usuarioService.ActualizarUsuarioAsync(usuario.ToEntity());

        if (usuarioActualizado == null)
        {
            _logService.Advertencia(nameof(UsuarioController), $"No se encontró el usuario con ID {usuarioId}.");
            return NotFound(ResponseDto<object>.Advertencia(ApiMensajesUsuarios.UsuarioNoEncontrado));
        }

        // ✅ Convertir Usuario (Entities) a UsuarioDto (DTOs) antes de devolverlo
        var usuarioActualizadoDto = usuarioActualizado.ToDto();

        _logService.Correcto(nameof(UsuarioController), $"Usuario actualizado correctamente: {usuarioId}.");
        return Ok(ResponseDto<UsuarioDto>.Exito(ApiMensajesUsuarios.UsuarioActualizado, usuarioActualizadoDto));
    }


    /// <summary>
    /// Realiza un borrado lógico de un usuario, cambiando su estado a inactivo.
    /// </summary>
    /// <param name="usuarioId">Identificador único del usuario a eliminar.</param>
    [HttpDelete("eliminarUsuario/{usuarioId}")]
    [ProducesUsuarioResponse(typeof(ResponseDto<UsuarioDto>), ApiDocumentacionUsuarios.EliminarUsuarioSummary, ApiDocumentacionUsuarios.EliminarUsuarioDescription)]
    public async Task<IActionResult> EliminarUsuario([FromRoute] string usuarioId)
    {
        _logService.Informacion(nameof(UsuarioController), $"Se solicita el eliminación del usuario {usuarioId}.");
        var usuarioEliminado = await _usuarioService.EliminarUsuarioAsync(usuarioId);

        if (usuarioEliminado == null)
        {
             _logService.Advertencia(nameof(UsuarioController), $" No se encontró el usuario con ID {usuarioId}.");
            return NotFound(ResponseDto<object>.Advertencia(ApiMensajesUsuarios.RegistroNoEncontrado));
        }

        _logService.Correcto(nameof(UsuarioController), $"Usuario eliminado correctamente: {usuarioId}.");
        return Ok(ResponseDto<UsuarioDto>.Exito(ApiMensajesUsuarios.UsuarioEliminado, usuarioEliminado.ToDto()));
    }
}
