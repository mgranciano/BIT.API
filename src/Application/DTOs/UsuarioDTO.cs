#nullable disable
namespace Application.DTOs;

/// <summary>
/// Representa un objeto de transferencia de datos (DTO) para la entidad `Usuario`.
/// </summary>
/// <remarks>
/// Se usa para enviar y recibir datos de usuario en la API, sin exponer la entidad completa.
/// </remarks>
public class UsuarioDTO
{
    /// <summary>
    /// Identificador único del usuario.
    /// </summary>
    public string UsuarioId { get; set; }

    /// <summary>
    /// Correo electrónico del usuario.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Nombre del usuario.
    /// </summary>
    public string Nombre { get; set; }

    /// <summary>
    /// Identificador del usuario superior (jefe o líder directo).
    /// </summary>
    public string UsuarioSuperiorId { get; set; }

    /// <summary>
    /// Nombre del usuario superior.
    /// </summary>
    public string NombreSuperior { get; set; }

    /// <summary>
    /// Correo electrónico del usuario superior.
    /// </summary>
    public string EmailSuperior { get; set; }

    /// <summary>
    /// Lista de países asignados al usuario.
    /// </summary>
    public List<PaisDTO> Paises { get; set; }

    /// <summary>
    /// Lista de roles asignados al usuario.
    /// </summary>
    public List<RolDTO> Roles { get; set; }

    /// <summary>
    /// Fecha en la que el usuario fue creado en el sistema.
    /// </summary>
    public DateTime FechaCreacion { get; set; }

    /// <summary>
    /// Última fecha de actualización del usuario en el sistema.
    /// </summary>
    public DateTime FechaActualizacion { get; set; }

    /// <summary>
    /// Estado actual del usuario (activo/inactivo).
    /// </summary>
    public bool Estatus { get; set; }
}

/// <summary>
/// Representa un objeto de transferencia de datos (DTO) para `Pais`.
/// </summary>
public class PaisDTO
{
    public string PaisId { get; set; }
    public bool Estado { get; set; }
}

/// <summary>
/// Representa un objeto de transferencia de datos (DTO) para `Rol`.
/// </summary>
public class RolDTO
{
    public string RolId { get; set; }
    public bool Estado { get; set; }
}
