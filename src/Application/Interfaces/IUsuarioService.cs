namespace Application.Interfaces;

using Dominio.Entities;
using Dominio.Entities.Dapper;

/// <summary>
/// Interfaz para el servicio de gestión de usuarios.
/// </summary>
public interface IUsuarioService
{
    Task<IEnumerable<Usuario>> ObtenerUsuariosAsync();
    Task<Usuario?> ObtenerUsuarioPorIdAsync(string usuarioId);
    Task<AccesoUsuario?> ObtenerUsuarioPorEmailAsync(string emial);
    Task<RespuestaDapper> RegistrarUsuarioAsync(Usuario usuario);
    Task<RespuestaDapper> ActualizarUsuarioAsync(Usuario usuario);
    Task<Usuario?> EliminarUsuarioAsync(string usuarioId);
    Task<IEnumerable<ModuloGeneral>> ObtenerModulosPorUsuarioAsync(string idUsuario);
}
