#nullable disable
using Application.DTOs;
using MediatR;
using Dominio.Interfaces;

namespace Application.UseCases.Usuarios.Commands;

/// <summary>
/// Representa un comando para eliminar un usuario del sistema.
/// </summary>
/// <remarks>
/// Este comando se utiliza en MediatR para solicitar la eliminación lógica de un usuario
/// a través del servicio de escritura de usuarios (`IUsuarioWriter`).
/// </remarks>
public class EliminarUsuarioCommand : IRequest<UsuarioDto>
{
    /// <summary>
    /// Identificador único del usuario que se desea eliminar.
    /// </summary>
    public string UsuarioId { get; set; }

    /// <summary>
    /// Constructor del comando para eliminar un usuario.
    /// </summary>
    /// <param name="usuarioId">El identificador del usuario a eliminar.</param>
    public EliminarUsuarioCommand(string usuarioId)
    {
        UsuarioId = usuarioId;
    }
}

/// <summary>
/// Manejador del comando `EliminarUsuarioCommand`.
/// </summary>
/// <remarks>
/// Este manejador utiliza el repositorio de escritura de usuarios (`IUsuarioWriter`) 
/// para procesar la solicitud y marcar el usuario como eliminado en el sistema.
/// </remarks>
public class EliminarUsuarioHandler : IRequestHandler<EliminarUsuarioCommand, UsuarioDto>
{
    private readonly IUsuarioWriter _usuarioWriter;

    /// <summary>
    /// Constructor del manejador para eliminar un usuario.
    /// </summary>
    /// <param name="usuarioWriter">Servicio para la escritura de usuarios.</param>
    public EliminarUsuarioHandler(IUsuarioWriter usuarioWriter)
    {
        _usuarioWriter = usuarioWriter;
    }

    /// <summary>
    /// Maneja la solicitud para eliminar un usuario.
    /// </summary>
    /// <param name="request">Solicitud de comando que contiene el identificador del usuario a eliminar.</param>
    /// <param name="cancellationToken">Token de cancelación de la operación.</param>
    /// <returns>El usuario eliminado en formato `UsuarioDto` si la operación es exitosa; de lo contrario, `null`.</returns>
    public async Task<UsuarioDto> Handle(EliminarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuarioEliminado = await _usuarioWriter.EliminarUsuarioAsync(request.UsuarioId);
        if (usuarioEliminado == null) return null;

        return new UsuarioDto
        {
            UsuarioId = usuarioEliminado.UsuarioId,
            Email = usuarioEliminado.Email,
            Nombre = usuarioEliminado.Nombre,
            UsuarioSuperiorId = usuarioEliminado.UsuarioSuperiorId,
            NombreSuperior = usuarioEliminado.NombreSuperior,
            EmailSuperior = usuarioEliminado.EmailSuperior,
            Paises = usuarioEliminado.Paises.Select(p => new PaisDto
            {
                PaisId = p.PaisId,
                Estado = p.Estado
            }).ToList(),
            Roles = usuarioEliminado.Roles.Select(r => new RolDto
            {
                RolId = r.RolId,
                Estado = r.Estado
            }).ToList(),
            FechaCreacion = usuarioEliminado.FechaCreacion,
            FechaActualizacion = usuarioEliminado.FechaActualizacion,
            Estatus = usuarioEliminado.Estatus
        };
    }
}
