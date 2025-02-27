using Dominio.Entities;

namespace Dominio.Interfaces;

/// <summary>
/// Define las operaciones completas para la entidad `Usuario`, combinando lectura y escritura.
/// </summary>
public interface IUsuarioRepository
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

    ///<summary>
    /// Valida si el usuario se encuentra registrado en el sistema por correo electronico
    ///</summary>
    /// <param name="correo">Correo del usuario al que se desea buscar</param>
    /// <returns>Si el usuario existe, se retorna el usuario, de lo contrario, se retorna null </returns>
    Task<DatosAccesoUsuario?> ObtenerUsuarioPorEmailAsync(string correo);

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
    /// <summary>
    /// Obtiene los modulos a los que tiene acceso un usuario
    /// </summary>
    /// <param name="idUsuario">Identificador del Usuario</param>
    /// <returns>Retorna una lista de modulos y submodulos al que tiene acceso el usuario</returns>
    Task<IEnumerable<ModuloGeneral>> ObtenerModulosPorUsuarioAsync(string idUsuario);
}
