namespace Application.Interfaces;

using Dominio.Entities;

/// <summary>
/// Interfaz para el servicio de gestión de usuarios.
/// </summary>
public interface IUsuarioService
{
    Task<IEnumerable<Usuario>> ObtenerUsuariosAsync();
    Task<Usuario?> ObtenerUsuarioPorIdAsync(string usuarioId);
    Task<Usuario?> ObtenerUsuarioPorEmailAsync(string emial);
    Task<Usuario> RegistrarUsuarioAsync(Usuario usuario);
    Task<Usuario?> ActualizarUsuarioAsync(Usuario usuario);
    Task<Usuario?> EliminarUsuarioAsync(string usuarioId);
}
