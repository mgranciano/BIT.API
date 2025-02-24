using Dominio.Entities;

namespace Dominio.Interfaces;

/// <summary>
/// Define las operaciones de escritura para la entidad `Usuario`.
/// </summary>
/// <remarks>
/// Esta interfaz proporciona métodos para la creación, modificación y eliminación 
/// de usuarios dentro del sistema.
/// </remarks>
public interface IUsuarioWriter
{
    /// <summary>
    /// Registra un nuevo usuario en el sistema.
    /// </summary>
    /// <param name="usuario">Objeto que contiene los datos del usuario a registrar.</param>
    /// <returns>El usuario registrado con sus datos almacenados.</returns>
    Task<Usuario> RegistrarUsuarioAsync(Usuario usuario);

    /// <summary>
    /// Actualiza la información de un usuario existente.
    /// </summary>
    /// <param name="usuario">Objeto con los datos actualizados del usuario.</param>
    /// <returns>El usuario actualizado si la operación es exitosa; de lo contrario, `null`.</returns>
    Task<Usuario?> ActualizarUsuarioAsync(Usuario usuario);

    /// <summary>
    /// Realiza un borrado lógico de un usuario, marcándolo como inactivo.
    /// </summary>
    /// <param name="usuarioId">El identificador del usuario a eliminar.</param>
    /// <returns>El usuario eliminado si la operación es exitosa; de lo contrario, `null`.</returns>
    Task<Usuario?> EliminarUsuarioAsync(string usuarioId);
}
