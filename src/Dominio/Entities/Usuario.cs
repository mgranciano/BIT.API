#nullable disable
namespace Dominio.Entities;

/// <summary>
/// Representa la entidad de un usuario en el sistema.
/// </summary>
/// <remarks>
/// Contiene la información general de un usuario, incluyendo su identificador, 
/// datos personales, jerarquía organizacional, roles y países asignados.
/// </remarks>
public class Usuario
{
    /// <summary>
    /// Identificador único del usuario.
    /// </summary>
    public string IdUsuario { get; set; }

    /// <summary>
    /// Correo electrónico del usuario.
    /// </summary>
    public string CorreoElectronico { get; set; }

    /// <summary>
    /// Nombre completo del usuario.
    /// </summary>
    public string NombreCompleto { get; set; }

    /// <summary>
    /// Identificador del usuario superior (jefe o líder directo).
    /// </summary>
    public string IdSuperior { get; set; }

    /// <summary>
    /// Nombre del usuario superior.
    /// </summary>
    public string NombreSuperior { get; set; }

    /// <summary>
    /// Correo electrónico del usuario superior.
    /// </summary>
    public string CorreoElectronicoSuperior { get; set; }

    /// <summary>
    /// Lista de países asignados al usuario.
    /// </summary>
    public List<Pais> Pais { get; set; }

    /// <summary>
    /// Lista de roles asignados al usuario.
    /// </summary>
    public List<Rol> Rol { get; set; }

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
    public bool Estado { get; set; }
}

/// <summary>
/// Representa un país asignado a un usuario.
/// </summary>
public class Pais
{
    /// <summary>
    /// Identificador único del país.
    /// </summary>
    public string PaisId { get; set; }

    /// <summary>
    /// Estado del país asignado al usuario (activo/inactivo).
    /// </summary>
    public bool Estado { get; set; }
}

/// <summary>
/// Representa un rol asignado a un usuario.
/// </summary>
public class Rol
{
    /// <summary>
    /// Identificador único del rol.
    /// </summary>
    public string RolId { get; set; }

    /// <summary>
    /// Estado del rol asignado al usuario (activo/inactivo).
    /// </summary>
    public bool Estado { get; set; }
}
