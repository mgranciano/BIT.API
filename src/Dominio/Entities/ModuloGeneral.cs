
namespace Dominio.Entities;

public class ModuloGeneral
{
    /// <summary>
    /// Identificador único del módulo.
    /// </summary>
    public int IdMenu { get; set; }
    /// <summary>
    /// Identificador del Modulo al que está referencia el Submodulo.
    /// </summary>
    public string? IdMenuCatalogo { get; set; }
    /// <summary>
    /// Nombre del Menú.
    /// </summary>
    public string Menu { get; set; }
    /// <summary>
    /// Ruta de la imagen del icono del menú.
    /// </summary>
    public string Icono { get; set; }
    /// <summary>
    /// Ruta de la pagina a la que se redirige el menú.
    /// </summary>
    public string Ruta { get; set; }

}
