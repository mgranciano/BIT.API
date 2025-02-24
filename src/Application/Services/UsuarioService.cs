namespace Application.Services;

using Application.Interfaces;
using Dominio.Entities;
using Dominio.Interfaces;

/// <summary>
/// Servicio que gestiona las operaciones relacionadas con los usuarios.
/// </summary>
public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioReader _usuarioReader;
    private readonly IUsuarioWriter _usuarioWriter;

    public UsuarioService(IUsuarioReader usuarioReader, IUsuarioWriter usuarioWriter)
    {
        _usuarioReader = usuarioReader;
        _usuarioWriter = usuarioWriter;
    }

    /// <summary>
    /// Obtiene la lista completa de usuarios.
    /// </summary>
    /// <returns>Una colección de usuarios.</returns>
    public async Task<IEnumerable<Usuario>> ObtenerUsuariosAsync()
    {
        return await _usuarioReader.ObtenerUsuariosAsync();
    }

    /// <summary>
    /// Obtiene un usuario por su identificador único.
    /// </summary>
    /// <param name="usuarioId">El identificador del usuario a buscar.</param>
    /// <returns>El usuario correspondiente si existe; de lo contrario, `null`.</returns>
    public async Task<Usuario?> ObtenerUsuarioPorIdAsync(string usuarioId)
    {
        return await _usuarioReader.ObtenerUsuarioPorIdAsync(usuarioId);
    }

    /// <summary>
    /// Registra un nuevo usuario en el sistema.
    /// </summary>
    /// <param name="usuario">Objeto que contiene la información del usuario a registrar.</param>
    /// <returns>El usuario registrado con sus datos almacenados.</returns>
    public async Task<Usuario> RegistrarUsuarioAsync(Usuario usuario)
    {
        return await _usuarioWriter.RegistrarUsuarioAsync(usuario);
    }

    /// <summary>
    /// Edita la información de un usuario existente.
    /// </summary>
    /// <param name="usuario">Objeto con los datos actualizados del usuario.</param>
    /// <returns>El usuario actualizado si la operación es exitosa; de lo contrario, `null`.</returns>
    public async Task<Usuario?> ActualizarUsuarioAsync(Usuario usuario)
    {
        return await _usuarioWriter.ActualizarUsuarioAsync(usuario);
    }

    /// <summary>
    /// Realiza un borrado lógico de un usuario, marcándolo como inactivo.
    /// </summary>
    /// <param name="usuarioId">El identificador del usuario a eliminar.</param>
    /// <returns>El usuario eliminado si la operación es exitosa; de lo contrario, `null`.</returns>
    public async Task<Usuario?> EliminarUsuarioAsync(string usuarioId)
    {
        return await _usuarioWriter.EliminarUsuarioAsync(usuarioId);
    }

    /// <summary>
    /// Obtiene un usuario por su correo electrónico.
    /// </summary>
    /// <param name="correo">Correo electronico del usuario</param>
    /// <returns></returns>

    public async Task<LogInUsuario> ObtenerPorCorreoAsync(string correo)
    {
        return await _usuarioReader.ObtenerUsuarioPorCorreoAsync(correo);
    }
}
