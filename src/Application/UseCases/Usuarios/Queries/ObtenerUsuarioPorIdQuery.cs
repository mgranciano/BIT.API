#nullable disable
using Application.DTOs;
using MediatR;
using Dominio.Interfaces;

namespace Application.UseCases.Usuarios.Queries;

/// <summary>
/// Representa una consulta para obtener un usuario por su identificador único.
/// </summary>
/// <remarks>
/// Esta consulta se utiliza en MediatR para solicitar los datos de un usuario específico
/// a través del servicio de lectura de usuarios (`IUsuarioReader`).
/// </remarks>
public class ObtenerUsuarioPorIdQuery : IRequest<UsuarioDto>
{
    /// <summary>
    /// Identificador único del usuario que se desea obtener.
    /// </summary>
    public string UsuarioId { get; set; }

    /// <summary>
    /// Constructor de la consulta para obtener un usuario por ID.
    /// </summary>
    /// <param name="usuarioId">El identificador del usuario a buscar.</param>
    public ObtenerUsuarioPorIdQuery(string usuarioId)
    {
        UsuarioId = usuarioId;
    }
}

/// <summary>
/// Manejador de la consulta `ObtenerUsuarioPorIdQuery`.
/// </summary>
/// <remarks>
/// Este manejador utiliza el repositorio de lectura de usuarios (`IUsuarioReader`) 
/// para procesar la solicitud y recuperar un usuario específico del sistema.
/// </remarks>
public class ObtenerUsuarioPorIdHandler : IRequestHandler<ObtenerUsuarioPorIdQuery, UsuarioDto>
{
    private readonly IUsuarioReader _usuarioReader;

    /// <summary>
    /// Constructor del manejador para obtener un usuario por ID.
    /// </summary>
    /// <param name="usuarioReader">Servicio para la lectura de usuarios.</param>
    public ObtenerUsuarioPorIdHandler(IUsuarioReader usuarioReader)
    {
        _usuarioReader = usuarioReader;
    }

    /// <summary>
    /// Maneja la solicitud para obtener un usuario por su identificador único.
    /// </summary>
    /// <param name="request">Solicitud de consulta que contiene el identificador del usuario.</param>
    /// <param name="cancellationToken">Token de cancelación de la operación.</param>
    /// <returns>El usuario encontrado en formato `UsuarioDto` si existe; de lo contrario, `null`.</returns>
    public async Task<UsuarioDto> Handle(ObtenerUsuarioPorIdQuery request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioReader.ObtenerUsuarioPorIdAsync(request.UsuarioId);
        if (usuario == null) return null;

        return new UsuarioDto
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
        };
    }
}
