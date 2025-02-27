namespace Application.Services;

using Application.Interfaces;
using Dominio.Entities;
using Dominio.Interfaces;

/// <summary>
/// Servicio que gestiona las operaciones relacionadas con los usuarios.
/// </summary>
public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;


    /// <summary>
    /// Constructor de UsuarioService.
    /// </summary>
    /// <param name="usuarioRepository">Servicio para CRUD de usuarios.</param>
    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    /// <summary>
    /// Obtiene la lista completa de usuarios.
    /// </summary>
    /// <returns>Una colección de usuarios.</returns>
    public async Task<IEnumerable<Usuario>> ObtenerUsuariosAsync()
    {
        return await _usuarioRepository.ObtenerUsuariosAsync();
    }

    /// <summary>
    /// Obtiene un usuario por su identificador único.
    /// </summary>
    /// <param name="usuarioId">El identificador del usuario a buscar.</param>
    /// <returns>El usuario correspondiente si existe; de lo contrario, `null`.</returns>
    public async Task<Usuario?> ObtenerUsuarioPorIdAsync(string usuarioId)
    {
        return await _usuarioRepository.ObtenerUsuarioPorIdAsync(usuarioId);
    }

    /// <summary>
    /// Registra un nuevo usuario en el sistema.
    /// </summary>
    /// <param name="usuario">Objeto que contiene la información del usuario a registrar.</param>
    /// <returns>El usuario registrado con sus datos almacenados.</returns>
    public async Task<Usuario> RegistrarUsuarioAsync(Usuario usuario)
    {
        return await _usuarioRepository.RegistrarUsuarioAsync(usuario);
    }

    /// <summary>
    /// Edita la información de un usuario existente.
    /// </summary>
    /// <param name="usuario">Objeto con los datos actualizados del usuario.</param>
    /// <returns>El usuario actualizado si la operación es exitosa; de lo contrario, `null`.</returns>
    public async Task<Usuario?> ActualizarUsuarioAsync(Usuario usuario)
    {
        return await _usuarioRepository.ActualizarUsuarioAsync(usuario);
    }

    /// <summary>
    /// Realiza un borrado lógico de un usuario, marcándolo como inactivo.
    /// </summary>
    /// <param name="usuarioId">El identificador del usuario a eliminar.</param>
    /// <returns>El usuario eliminado si la operación es exitosa; de lo contrario, `null`.</returns>
    public async Task<Usuario?> EliminarUsuarioAsync(string usuarioId)
    {
        return await _usuarioRepository.EliminarUsuarioAsync(usuarioId);
    }

    /// <summary>
    /// Obtiene un usuario por su correo electrónico.
    /// </summary>
    /// <param name="correo">Correo electronico del usuario</param>
    /// <returns>Retorna el usuario</returns> 

    public async Task<DatosAccesoUsuario?> ObtenerUsuarioPorEmailAsync(string emial)
    {
        return await _usuarioRepository.ObtenerUsuarioPorEmailAsync(emial);
    }
    /// <summary>
    /// Obtiene los módulos a los que tiene acceso un usuario.
    /// </summary>
    /// <param name="idUsuario">Identificador del usaurio con el que se hará la validación para el acceso a los modulos</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<List<Modulo>> ObtenerModulosPorUsuarioAsync(string idUsuario)
    {
        IEnumerable<ModuloGeneral> modulos = await _usuarioRepository.ObtenerModulosPorUsuarioAsync(idUsuario);
        List<Modulo> listaModulos = null;
        if (modulos != null && modulos.Any())
        {
            listaModulos = new List<Modulo>();
            List<ModuloGeneral> subModulos = modulos.Where(x => !string.IsNullOrEmpty(x.IdMenuCatalogo)).ToList();
            foreach (var modulo in modulos)
            {

                if (string.IsNullOrEmpty(modulo.IdMenuCatalogo))
                {
                    Modulo mod = new Modulo();
                    mod.Label = modulo.Menu;
                    mod.Icono = modulo.Icono;
                    mod.Ruta = modulo.Ruta;
                    var subM = subModulos.Where(x => Convert.ToInt16(x.IdMenuCatalogo) == modulo.IdMenu).ToList();
                    if (subM.Any())
                    {
                        List<Modulo> listaSubModulos = new List<Modulo>();
                        foreach (var subModulo in subM)
                        {
                            Modulo subMod = new Modulo();
                            subMod.Label = subModulo.Menu;
                            subMod.Icono = subModulo.Icono;
                            subMod.Ruta = subModulo.Ruta;
                            listaSubModulos.Add(subMod);
                        }
                        mod.Submodulos = listaSubModulos;
                    }
                    listaModulos.Add(mod);
                }
            }
        }
        return listaModulos;
    }
}
