using Application.DTOs;
using Dominio.Entities;
using MediatR;
using Dominio.Interfaces;

namespace Application.UseCases.Usuarios.Commands;

/// <summary>
/// Representa un comando para dar de alta un nuevo usuario en el sistema.
/// </summary>
/// <remarks>
/// Este comando utiliza `UsuarioDto` para recibir los datos del usuario y 
/// delegar la creación de la entidad `Usuario` al manejador.
/// </remarks>
public class AltaUsuarioCommand : IRequest<UsuarioDto>
{
    /// <summary>
    /// Objeto que contiene la información del usuario a registrar.
    /// </summary>
    public UsuarioDto Usuario { get; set; }

    /// <summary>
    /// Constructor del comando de alta de usuario.
    /// </summary>
    /// <param name="usuario">Objeto que contiene los datos del usuario a registrar.</param>
    public AltaUsuarioCommand(UsuarioDto usuario)
    {
        Usuario = usuario;
    }
}

/// <summary>
/// Manejador del comando `AltaUsuarioCommand`.
/// </summary>
/// <remarks>
/// Este manejador convierte `UsuarioDto` en una entidad `Usuario`, 
/// lo guarda en el repositorio y devuelve `UsuarioDto`.
/// </remarks>
public class AltaUsuarioHandler : IRequestHandler<AltaUsuarioCommand, UsuarioDto>
{
    private readonly IUsuarioWriter _usuarioWriter;

    /// <summary>
    /// Constructor del manejador de alta de usuario.
    /// </summary>
    /// <param name="usuarioWriter">Servicio para la escritura de usuarios.</param>
    public AltaUsuarioHandler(IUsuarioWriter usuarioWriter)
    {
        _usuarioWriter = usuarioWriter;
    }

    /// <summary>
    /// Maneja la solicitud para registrar un nuevo usuario.
    /// </summary>
    /// <param name="request">Solicitud de comando que contiene los datos del usuario.</param>
    /// <param name="cancellationToken">Token de cancelación de la operación.</param>
    /// <returns>El usuario registrado en formato `UsuarioDto`.</returns>
    public async Task<UsuarioDto> Handle(AltaUsuarioCommand request, CancellationToken cancellationToken)
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
            FechaCreacion = DateTime.UtcNow,
            FechaActualizacion = DateTime.UtcNow,
            Estatus = true
        };

        var usuarioCreado = await _usuarioWriter.RegistrarUsuarioAsync(usuario);

        return new UsuarioDto
        {
            UsuarioId = usuarioCreado.UsuarioId,
            Email = usuarioCreado.Email,
            Nombre = usuarioCreado.Nombre,
            UsuarioSuperiorId = usuarioCreado.UsuarioSuperiorId,
            NombreSuperior = usuarioCreado.NombreSuperior,
            EmailSuperior = usuarioCreado.EmailSuperior,
            Paises = usuarioCreado.Paises.Select(p => new PaisDto
            {
                PaisId = p.PaisId,
                Estado = p.Estado
            }).ToList(),
            Roles = usuarioCreado.Roles.Select(r => new RolDto
            {
                RolId = r.RolId,
                Estado = r.Estado
            }).ToList(),
            FechaCreacion = usuarioCreado.FechaCreacion,
            FechaActualizacion = usuarioCreado.FechaActualizacion,
            Estatus = usuarioCreado.Estatus
        };
    }
}
