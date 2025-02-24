using Dominio.Entities;

namespace Dominio.Interfaces;

/// <summary>
/// Define las operaciones de lectura para la entidad `Usuario`.
/// </summary>
/// <remarks>
/// Esta interfaz proporciona métodos para la consulta de usuarios en el sistema, 
/// permitiendo obtener la lista completa de usuarios o un usuario específico por su identificador.
/// </remarks>
public interface IUsuarioReader
{
    /// <summary>
    /// Obtiene la lista completa de usuarios registrados en el sistema.
    /// </summary>
    /// <returns>Una colección de usuarios.</returns>
    Task<IEnumerable<Usuario>> ObtenerUsuariosAsync();

    /// <summary>
    /// Obtiene un usuario por su identificador único.
    /// </summary>
    /// <param name="usuarioId">El identificador del usuario a buscar.</param>
    /// <returns>El usuario correspondiente si existe; de lo contrario, `null`.</returns>
    Task<Usuario?> ObtenerUsuarioPorIdAsync(string usuarioId);
}
