
#nullable disable
namespace Dominio.Entities
{
    /// <summary>
    /// Representa la entidad de un usuario Valido del sistema.
    /// </summary>
    /// <remarks>
    /// Contiene la información general de un usuario, 
    /// datos personales, jerarquía organizacional, roles y países asignados.
    /// </remarks>
    public class LogInUsuario
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
        /// Identificador del superior al que está asignado el usuario
        /// </summary>
        public string IdSuperior { get; set; }
        /// <summary>
        /// Nombre del superior del usuario al que está asignado
        /// </summary>
        public string NombreSuperior { get; set; }
        /// <summary>
        /// Correo electronico del superior al que está asignado el usuario
        /// </summary>
        public string CorreoElectronicoSuperior { get; set; }
        /// <summary>
        /// Estado en el que se encuentra el usuario, Si el valor Es true, usuario Activo, de lo contrario, usuario inactivo
        /// </summary>
        public bool Estado { get; set; }

    }
}
