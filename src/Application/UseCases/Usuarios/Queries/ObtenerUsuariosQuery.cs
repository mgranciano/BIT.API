using Application.DTOs;
using MediatR;
using Dominio.Interfaces;

namespace Application.UseCases.Usuarios.Queries;

/// <summary>
/// Representa una consulta para obtener la lista de todos los usuarios.
/// </summary>
/// <remarks>
/// Esta consulta se utiliza en MediatR para solicitar la lista de usuarios 
/// desde el repositorio correspondiente.
/// </remarks>
public class ObtenerUsuariosQuery : IRequest<IEnumerable<UsuarioDto>> { }

/// <summary>
/// Manejador de la consulta `ObtenerUsuariosQuery`.
/// </summary>
/// <remarks>
/// Este manejador utiliza el repositorio de usuarios para recuperar la lista completa de usuarios
/// y los convierte en objetos `UsuarioDto`.
/// </remarks>
public class ObtenerUsuariosHandler : IRequestHandler<ObtenerUsuariosQuery, IEnumerable<UsuarioDto>>
{
    private readonly IUsuarioReader _usuarioReader;

    /// <summary>
    /// Constructor del manejador de la consulta para obtener usuarios.
    /// </summary>
    /// <param name="usuarioReader">Servicio para la lectura de usuarios.</param>
    public ObtenerUsuariosHandler(IUsuarioReader usuarioReader)
    {
        _usuarioReader = usuarioReader;
    }

    /// <summary>
    /// Maneja la solicitud de consulta para obtener la lista de usuarios.
    /// </summary>
    /// <param name="request">Solicitud de consulta.</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>Lista de usuarios disponibles en formato `UsuarioDto`.</returns>
    public async Task<IEnumerable<UsuarioDto>> Handle(ObtenerUsuariosQuery request, CancellationToken cancellationToken)
    {
        var usuarios = await _usuarioReader.ObtenerUsuariosAsync();
        
        return usuarios.Select(usuario => new UsuarioDto
        {
            UsuarioId = usuario.UsuarioId,
            Email = usuario.Email,
            Nombre = usuario.Nombre,
            UsuarioSuperiorId = usuario.UsuarioSuperiorId,
            NombreSuperior = usuario.NombreSuperior,
            EmailSuperior = usuario.EmailSuperior,
            Paises = usuario.Paises.Select(p => new PaisDto
            {
                PaisId = p.PaisId,
                Estado = p.Estado
            }).ToList(),
            Roles = usuario.Roles.Select(r => new RolDto
            {
                RolId = r.RolId,
                Estado = r.Estado
            }).ToList(),
            FechaCreacion = usuario.FechaCreacion,
            FechaActualizacion = usuario.FechaActualizacion,
            Estatus = usuario.Estatus
        }).ToList();
    }
}
