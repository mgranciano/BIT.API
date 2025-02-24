#nullable disable
using Application.DTOs;
using Dominio.Entities;
using MediatR;
using Dominio.Interfaces;

namespace Application.UseCases.Usuarios.Commands;

/// <summary>
/// Representa un comando para actualizar la información de un usuario existente.
/// </summary>
/// <remarks>
/// Este comando utiliza `UsuarioDto` para recibir los datos del usuario y 
/// delegar la actualización de la entidad `Usuario` al manejador.
/// </remarks>
public class ActualizarUsuarioCommand : IRequest<UsuarioDto>
{
    /// <summary>
    /// Datos del usuario a actualizar.
    /// </summary>
    public UsuarioDto Usuario { get; set; }

    /// <summary>
    /// Constructor del comando para actualizar un usuario.
    /// </summary>
    /// <param name="usuario">Objeto con los datos actualizados del usuario.</param>
    public ActualizarUsuarioCommand(UsuarioDto usuario)
    {
        Usuario = usuario;
    }
}

/// <summary>
/// Manejador del comando `ActualizarUsuarioCommand`.
/// </summary>
/// <remarks>
/// Este manejador convierte `UsuarioDto` en una entidad `Usuario`, 
/// lo actualiza en el repositorio y devuelve `UsuarioDto`.
/// </remarks>
public class ActualizarUsuarioHandler : IRequestHandler<ActualizarUsuarioCommand, UsuarioDto>
{
    private readonly IUsuarioWriter _usuarioWriter;

    /// <summary>
    /// Constructor del manejador para actualizar un usuario.
    /// </summary>
    /// <param name="usuarioWriter">Servicio para la escritura de usuarios.</param>
    public ActualizarUsuarioHandler(IUsuarioWriter usuarioWriter)
    {
        _usuarioWriter = usuarioWriter;
    }

    /// <summary>
    /// Maneja la solicitud para actualizar un usuario existente.
    /// </summary>
    /// <param name="request">Solicitud de comando que contiene los datos actualizados del usuario.</param>
    /// <param name="cancellationToken">Token de cancelación de la operación.</param>
    /// <returns>El usuario actualizado en formato `UsuarioDto` si la operación es exitosa; de lo contrario, `null`.</returns>
    public async Task<UsuarioDto> Handle(ActualizarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = new Usuario
        {
            UsuarioId = request.Usuario.UsuarioId,
            Email = request.Usuario.Email,
            Nombre = request.Usuario.Nombre,
            UsuarioSuperiorId = request.Usuario.UsuarioSuperiorId,
            NombreSuperior = request.Usuario.NombreSuperior,
            EmailSuperior = request.Usuario.EmailSuperior,
            Paises = request.Usuario.Paises.Select(p => new Pais
            {
                PaisId = p.PaisId,
                Estado = p.Estado
            }).ToList(),
            Roles = request.Usuario.Roles.Select(r => new Rol
            {
                RolId = r.RolId,
                Estado = r.Estado
            }).ToList(),
            FechaActualizacion = DateTime.UtcNow,
            Estatus = request.Usuario.Estatus
        };

        var usuarioActualizado = await _usuarioWriter.ActualizarUsuarioAsync(usuario);
        if (usuarioActualizado == null) return null;

        return new UsuarioDto
        {
            UsuarioId = usuarioActualizado.UsuarioId,
            Email = usuarioActualizado.Email,
            Nombre = usuarioActualizado.Nombre,
            UsuarioSuperiorId = usuarioActualizado.UsuarioSuperiorId,
            NombreSuperior = usuarioActualizado.NombreSuperior,
            EmailSuperior = usuarioActualizado.EmailSuperior,
            Paises = usuarioActualizado.Paises.Select(p => new PaisDto
            {
                PaisId = p.PaisId,
                Estado = p.Estado
            }).ToList(),
            Roles = usuarioActualizado.Roles.Select(r => new RolDto
            {
                RolId = r.RolId,
                Estado = r.Estado
            }).ToList(),
            FechaCreacion = usuarioActualizado.FechaCreacion,
            FechaActualizacion = usuarioActualizado.FechaActualizacion,
            Estatus = usuarioActualizado.Estatus
        };
    }
}
