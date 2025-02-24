using API.Attributes;
using Application.Constants;
using Application.DTOs;
using Application.UseCases.Usuarios.Commands;
using Application.UseCases.Usuarios.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
public class UsuarioController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UsuarioController> _logger;

    /// <summary>
    /// Constructor del controlador de usuarios.
    /// </summary>
    /// <param name="mediator">Instancia de MediatR para manejar las solicitudes CQRS.</param>
    /// <param name="logger">Instancia de Serilog para registrar eventos.</param>
    public UsuarioController(IMediator mediator, ILogger<UsuarioController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene la lista completa de usuarios registrados en el sistema.
    /// </summary>
    [HttpGet("obtenerUsuarios")]
    [ProducesUsuarioResponse(typeof(ResponseDTO<IEnumerable<UsuarioDTO>>), ApiDocumentacionUsuarios.ObtenerUsuariosSummary, ApiDocumentacionUsuarios.ObtenerUsuariosDescription)]
    public async Task<IActionResult> ObtenerUsuarios()
    {
        _logger.LogInformation("📢 [INFO] Se solicitó obtener todos los usuarios.");

        var usuarios = await _mediator.Send(new ObtenerUsuariosQuery());

        if (usuarios == null || !usuarios.Any())
        {
            _logger.LogWarning("⚠️ [WARNING] No se encontraron usuarios.");
            return NotFound(ResponseDTO<object>.Advertencia(ApiMensajesUsuarios.UsuariosNoEncontrados));
        }

        _logger.LogInformation("✅ [SUCCESS] Usuarios obtenidos correctamente.");
        return Ok(ResponseDTO<IEnumerable<UsuarioDTO>>.Exito(ApiMensajesUsuarios.UsuariosEncontrados, usuarios));
    }

    /// <summary>
    /// Obtiene un usuario por su identificador único.
    /// </summary>
    /// <param name="usuarioId">Identificador único del usuario.</param>
    [HttpGet("obtenerUsuario/{usuarioId}")]
    [ProducesUsuarioResponse(typeof(ResponseDTO<UsuarioDTO>), ApiDocumentacionUsuarios.ObtenerUsuarioSummary, ApiDocumentacionUsuarios.ObtenerUsuarioDescription)]
    public async Task<IActionResult> ObtenerUsuario([FromRoute] string usuarioId)
    {
        _logger.LogInformation("📢 [INFO] Se solicitó obtener el usuario con ID {UsuarioId}.", usuarioId);

        var usuario = await _mediator.Send(new ObtenerUsuarioPorIdQuery(usuarioId));

        if (usuario == null)
        {
            _logger.LogWarning("⚠️ [WARNING] No se encontró el usuario con ID {UsuarioId}.", usuarioId);
            return NotFound(ResponseDTO<object>.Advertencia(ApiMensajesUsuarios.UsuarioNoEncontrado));
        }

        _logger.LogInformation("✅ [SUCCESS] Usuario encontrado: {UsuarioId}.", usuarioId);
        return Ok(ResponseDTO<UsuarioDTO>.Exito(ApiMensajesUsuarios.UsuarioEncontrado, usuario));
    }

    /// <summary>
    /// Registra un nuevo usuario en el sistema.
    /// </summary>
    /// <param name="usuario">Objeto con la información del usuario a registrar.</param>
    [HttpPost("altaUsuario")]
    [ProducesUsuarioResponse(typeof(ResponseDTO<UsuarioDTO>), ApiDocumentacionUsuarios.AltaUsuarioSummary, ApiDocumentacionUsuarios.AltaUsuarioDescription)]
    public async Task<IActionResult> AltaUsuario([FromBody] UsuarioDTO usuario)
    {
        if (usuario == null || string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Nombre))
        {
            _logger.LogWarning("⚠️ [WARNING] Datos inválidos en el registro de usuario.");
            return BadRequest(ResponseDTO<object>.Error(ApiMensajesUsuarios.DatosInvalidos));
        }

        _logger.LogInformation("📢 [INFO] Se está registrando un nuevo usuario.");
        var nuevoUsuario = await _mediator.Send(new AltaUsuarioCommand(usuario));

        _logger.LogInformation("✅ [SUCCESS] Usuario registrado correctamente: {UsuarioId}.", nuevoUsuario.UsuarioId);
        return CreatedAtAction(nameof(ObtenerUsuario), new { usuarioId = nuevoUsuario.UsuarioId },
            ResponseDTO<UsuarioDTO>.Exito(ApiMensajesUsuarios.UsuarioCreado, nuevoUsuario));
    }

    /// <summary>
    /// Actualiza parcialmente un usuario existente.
    /// </summary>
    /// <param name="usuarioId">Identificador único del usuario a actualizar.</param>
    /// <param name="usuario">Objeto con los datos a modificar.</param>
    [HttpPatch("actualizarUsuario/{usuarioId}")]
    [ProducesUsuarioResponse(typeof(ResponseDTO<UsuarioDTO>), ApiDocumentacionUsuarios.ActualizarUsuarioSummary, ApiDocumentacionUsuarios.ActualizarUsuarioDescription)]
    public async Task<IActionResult> ActualizarUsuario([FromRoute] string usuarioId, [FromBody] UsuarioDTO usuario)
    {
        if (usuarioId != usuario.UsuarioId)
        {
            _logger.LogWarning("⚠️ [WARNING] ID de usuario en URL no coincide con el cuerpo de la solicitud.");
            return BadRequest(ResponseDTO<object>.Error(ApiMensajesUsuarios.IdNoCoincide));
        }

        _logger.LogInformation("📢 [INFO] Se está actualizando el usuario con ID {UsuarioId}.", usuarioId);
        var usuarioActualizado = await _mediator.Send(new ActualizarUsuarioCommand(usuario));

        if (usuarioActualizado == null)
        {
            _logger.LogWarning("⚠️ [WARNING] No se encontró el usuario con ID {UsuarioId}.", usuarioId);
            return NotFound(ResponseDTO<object>.Advertencia(ApiMensajesUsuarios.UsuarioNoEncontrado));
        }

        _logger.LogInformation("✅ [SUCCESS] Usuario actualizado correctamente: {UsuarioId}.", usuarioId);
        return Ok(ResponseDTO<UsuarioDTO>.Exito(ApiMensajesUsuarios.UsuarioActualizado, usuarioActualizado));
    }

    /// <summary>
    /// Realiza un borrado lógico de un usuario, cambiando su estado a inactivo.
    /// </summary>
    /// <param name="usuarioId">Identificador único del usuario a eliminar.</param>
    [HttpDelete("eliminarUsuario/{usuarioId}")]
    [ProducesUsuarioResponse(typeof(ResponseDTO<UsuarioDTO>), ApiDocumentacionUsuarios.EliminarUsuarioSummary, ApiDocumentacionUsuarios.EliminarUsuarioDescription)]
    public async Task<IActionResult> EliminarUsuario([FromRoute] string usuarioId)
    {
        _logger.LogInformation("📢 [INFO] Se está eliminando lógicamente el usuario con ID {UsuarioId}.", usuarioId);
        var usuarioEliminado = await _mediator.Send(new EliminarUsuarioCommand(usuarioId));

        if (usuarioEliminado == null)
        {
            _logger.LogWarning("⚠️ [WARNING] No se encontró el usuario con ID {UsuarioId}.", usuarioId);
            return NotFound(ResponseDTO<object>.Advertencia(ApiMensajesUsuarios.RegistroNoEncontrado));
        }

        _logger.LogInformation("✅ [SUCCESS] Usuario eliminado lógicamente: {UsuarioId}.", usuarioId);
        return Ok(ResponseDTO<UsuarioDTO>.Exito(ApiMensajesUsuarios.UsuarioEliminado, usuarioEliminado));
    }
}
