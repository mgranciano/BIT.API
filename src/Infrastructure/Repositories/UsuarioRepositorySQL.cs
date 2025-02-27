using Dominio.Entities;
using Dominio.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>
/// Repositorio que gestiona la persistencia de usuarios en una base de datos SQL.
/// </summary>
/// <remarks>
/// Implementa `IUsuarioRepository` para proporcionar almacenamiento y gestión de datos 
/// de usuario utilizando Entity Framework Core con SQL Server.
/// </remarks>
public class UsuarioRepositorySQL : IUsuarioRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Constructor del repositorio de usuarios basado en SQL.
    /// </summary>
    /// <param name="context">Contexto de base de datos de la aplicación.</param>
    public UsuarioRepositorySQL(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todos los usuarios almacenados en la base de datos.
    /// </summary>
    /// <returns>Una colección de usuarios registrados en la base de datos.</returns>
    public async Task<IEnumerable<Usuario>> ObtenerUsuariosAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    /// <summary>
    /// Obtiene un usuario por su identificador único.
    /// </summary>
    /// <param name="usuarioId">El identificador del usuario a buscar.</param>
    /// <returns>El usuario correspondiente si existe; de lo contrario, `null`.</returns>
    public async Task<Usuario?> ObtenerUsuarioPorIdAsync(string usuarioId)
    {
        return await _context.Usuarios.FindAsync(usuarioId);
    }

    /// <summary>
    /// Registra un nuevo usuario en la base de datos.
    /// </summary>
    /// <param name="usuario">Objeto con los datos del usuario a registrar.</param>
    /// <returns>El usuario registrado con sus datos almacenados.</returns>
    public async Task<Usuario> RegistrarUsuarioAsync(Usuario usuario)
    {
        usuario.FechaCreacion = DateTime.UtcNow;
        usuario.FechaActualizacion = DateTime.UtcNow;
        usuario.Estado = true;

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    /// <summary>
    /// Actualiza la información de un usuario existente en la base de datos.
    /// </summary>
    /// <param name="usuario">Objeto con los datos actualizados del usuario.</param>
    /// <returns>El usuario actualizado si la operación es exitosa; de lo contrario, `null`.</returns>
    public async Task<Usuario?> ActualizarUsuarioAsync(Usuario usuario)
    {
        var usuarioExistente = await _context.Usuarios.FindAsync(usuario.IdUsuario);

        if (usuarioExistente != null)
        {
            usuarioExistente.CorreoElectronico = usuario.CorreoElectronico;
            usuarioExistente.NombreCompleto = usuario.NombreCompleto;
            usuarioExistente.IdSuperior = usuario.IdSuperior;
            usuarioExistente.NombreSuperior = usuario.NombreSuperior;
            usuarioExistente.CorreoElectronicoSuperior = usuario.CorreoElectronicoSuperior;
            usuarioExistente.Pais = usuario.Pais;
            usuarioExistente.Rol = usuario.Rol;
            usuarioExistente.FechaActualizacion = DateTime.UtcNow;
            usuarioExistente.Estado = usuario.Estado;

            await _context.SaveChangesAsync();
            return usuarioExistente;
        }
        return null;
    }

    /// <summary>
    /// Realiza un borrado lógico de un usuario, marcándolo como inactivo en la base de datos.
    /// </summary>
    /// <param name="usuarioId">El identificador del usuario a eliminar.</param>
    /// <returns>El usuario eliminado si la operación es exitosa; de lo contrario, `null`.</returns>
    public async Task<Usuario?> EliminarUsuarioAsync(string usuarioId)
    {
        var usuario = await _context.Usuarios.FindAsync(usuarioId);
        if (usuario != null)
        {
            usuario.Estado = false;
            usuario.FechaActualizacion = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return usuario;
        }
        return null;
    }

    /// <summary>
    /// Busca un usuario en el sistema utilizando su dirección de correo electrónico.
    /// </summary>
    /// <param name="correo">El correo electrónico del usuario a buscar.</param>
    /// <returns>El usuario encontrado si existe; de lo contrario, `null`.</returns>
    public async Task<DatosAccesoUsuario?> ObtenerUsuarioPorEmailAsync(string correo)
    {
        return new DatosAccesoUsuario();
        //return await _context.Usuarios
        //    .AsNoTracking() 
        //    .FirstOrDefaultAsync(u => u.Email == email);
    }

    public Task<IEnumerable<ModuloGeneral>> ObtenerModulosPorUsuarioAsync(string idUsuario)
    {
        throw new NotImplementedException();
    }
}
