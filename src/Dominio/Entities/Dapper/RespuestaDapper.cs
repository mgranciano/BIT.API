#nullable disable
namespace Dominio.Entities.Dapper
{


    /// <summary>
    /// Objeto que contiene la respuesta de una consulta Dapper.
    /// </summary>
    public class RespuestaDapper
    {
        /// <summary>
        /// Mensaje de la respuesta.
        /// </summary>
        public string Mensaje { get; set; }
        /// <summary>
        /// Estado de la respuesta. true si fue exitosa; de lo contrario, false.
        /// </summary>
        public string Estatus { get; set; }
        /// <summary>
        /// Objeto que contiene la respuesta de la consulta.
        /// </summary>
        public string Objeto { get; set; }
    }
}