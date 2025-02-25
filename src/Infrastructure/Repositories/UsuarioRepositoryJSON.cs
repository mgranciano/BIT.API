using Dominio.Entities;
using Dominio.Interfaces;
using System.Text.Json;

namespace Infrastructure.Repositories;

/// <summary>
/// Repositorio que gestiona la persistencia de usuarios en un archivo JSON.
/// </summary>
/// <remarks>
/// Implementa `IUsuarioRepository` para proporcionar almacenamiento y gestión de datos 
/// de usuario en un archivo JSON local.
/// </remarks>
public class UsuarioRepositoryJSON : IUsuarioRepository
{
    private readonly string _jsonFilePath;

    /// <summary>
    /// Constructor del repositorio de usuarios basado en JSON.
    /// </summary>
    public UsuarioRepositoryJSON()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        _jsonFilePath = Path.Combine(basePath, "Data", "usuarios.json");

        Console.WriteLine($"📂 Ruta JSON: {_jsonFilePath}");
    }

    /// <summary>
    /// Carga la lista de usuarios desde el archivo JSON.
    /// </summary>
    /// <returns>Una lista de usuarios almacenados en el archivo JSON.</returns>
    private async Task<List<Usuario>> CargarUsuariosAsync()
    {
        if (!File.Exists(_jsonFilePath))
        {
            return new List<Usuario>();
        }

        var json = await File.ReadAllTextAsync(_jsonFilePath);
        return JsonSerializer.Deserialize<List<Usuario>>(json) ?? new List<Usuario>();
    }

    /// <summary>
    /// Guarda la lista de usuarios en el archivo JSON.
    /// </summary>
    /// <param name="usuarios">Lista de usuarios a almacenar.</param>
    private async Task GuardarUsuariosAsync(List<Usuario> usuarios)
    {
        var json = JsonSerializer.Serialize(usuarios, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_jsonFilePath, json);
    }

    /// <summary>
    /// Obtiene todos los usuarios almacenados en el sistema.
    /// </summary>
    /// <returns>Una colección de usuarios registrados en el archivo JSON.</returns>
    public async Task<IEnumerable<Usuario>> ObtenerUsuariosAsync()
    {
        return await CargarUsuariosAsync();
    }

    /// <summary>
    /// Obtiene un usuario por su identificador único.
    /// </summary>
    /// <param name="usuarioId">El identificador del usuario a buscar.</param>
    /// <returns>El usuario correspondiente si existe; de lo contrario, `null`.</returns>
    public async Task<Usuario?> ObtenerUsuarioPorIdAsync(string usuarioId)
    {
        var usuarios = await CargarUsuariosAsync();
        return usuarios.FirstOrDefault(u => u.UsuarioId == usuarioId);
    }

    /// <summary>
    /// Registra un nuevo usuario en el sistema.
    /// </summary>
    /// <param name="usuario">Objeto con los datos del usuario a registrar.</param>
    /// <returns>El usuario registrado con sus datos almacenados.</returns>
    public async Task<Usuario> RegistrarUsuarioAsync(Usuario usuario)
    {
        var usuarios = await CargarUsuariosAsync();
        usuario.FechaCreacion = DateTime.UtcNow;
        usuario.FechaActualizacion = DateTime.UtcNow;
        usuario.Estatus = true;
        usuarios.Add(usuario);
        await GuardarUsuariosAsync(usuarios);
        return usuario;
    }

    /// <summary>
    /// Edita la información de un usuario existente.
    /// </summary>
    /// <param name="usuario">Objeto con los datos actualizados del usuario.</param>
    /// <returns>El usuario actualizado si la operación es exitosa; de lo contrario, `null`.</returns>
    public async Task<Usuario?> ActualizarUsuarioAsync(Usuario usuario)
    {
        var usuarios = await CargarUsuariosAsync();
        var usuarioExistente = usuarios.FirstOrDefault(u => u.UsuarioId == usuario.UsuarioId);

        if (usuarioExistente != null)
        {
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.UsuarioSuperiorId = usuario.UsuarioSuperiorId;
            usuarioExistente.NombreSuperior = usuario.NombreSuperior;
            usuarioExistente.EmailSuperior = usuario.EmailSuperior;
            usuarioExistente.Paises = usuario.Paises;
            usuarioExistente.Roles = usuario.Roles;
            usuarioExistente.FechaActualizacion = DateTime.UtcNow;
            usuarioExistente.Estatus = usuario.Estatus;

            await GuardarUsuariosAsync(usuarios);
            return usuarioExistente;
        }
        return null;
    }

    /// <summary>
    /// Realiza un borrado lógico de un usuario, marcándolo como inactivo.
    /// </summary>
    /// <param name="usuarioId">El identificador del usuario a eliminar.</param>
    /// <returns>El usuario eliminado si la operación es exitosa; de lo contrario, `null`.</returns>
    public async Task<Usuario?> EliminarUsuarioAsync(string usuarioId)
    {
        var usuarios = await CargarUsuariosAsync();
        var usuario = usuarios.FirstOrDefault(u => u.UsuarioId == usuarioId);
        if (usuario != null)
        {
            usuario.Estatus = false;
            usuario.FechaActualizacion = DateTime.UtcNow;
            await GuardarUsuariosAsync(usuarios);
            return usuario;
        }
        return null;
    }

    public async Task<Usuario?> ObtenerUsuarioPorEmailAsync(string email)
    {
        var usuarios = await CargarUsuariosAsync();
        return usuarios.FirstOrDefault(u => u.Email == email);
    }
}
