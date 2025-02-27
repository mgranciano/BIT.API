#nullable disable
using System.Collections.Generic;

namespace Dominio.Entities
{
    /// <summary>
    /// Representa un módulo en el sistema.
    /// </summary>
    public class Modulo
    {
        /// <summary>
        /// Nombre del módulo.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Ruta de la imagen del icono.
        /// </summary>
        public string Icono { get; set; }

        /// <summary>
        /// Ruta en la aplicación del modulo.
        /// </summary>
        public string Ruta { get; set; }

        /// <summary>
        /// Lista de submódulos asociados a este módulo.
        /// </summary>
        public List<Modulo>? Submodulos { get; set; }
    }
}
