#nullable disable
namespace Application.DTOs.Login
{
  /// <summary>
    /// Representa la entidad de un usuario Valido del sistema.
    /// </summary>
    /// <remarks>
    /// Contiene la información general de un usuario, 
    /// datos personales, jerarquía organizacional, roles y países asignados.
    /// </remarks>
    public class DatosAccesoUsuarioDto
    {
        /// <summary>
        /// Identificador del usuario
        /// </summary>
        public string IdUsuario { get; set; }
        /// <summary>
        /// Nombre, apellido parterno y Apellido materno del usuario
        /// </summary>
        public string NombreCompleto { get; set; }
        /// <summary>
        /// Correo electronico del usuario asociado a la compania
        /// </summary>
        public string CorreoElectronico { get; set; }

        /// <summary>
        /// Paises asignados al usuario
        /// </summary>
        public string Pais { get; set; }
        /// <summary>
        /// Rol asignado al usuario
        /// </summary>
        public string Rol { get; set; }
        /// <summary>
        /// Indica si el usuario esta activo o inactivo
        /// </summary>
        public bool Estado { get; set; }

    }
}