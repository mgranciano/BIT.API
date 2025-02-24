namespace Dominio.Interfaces;

/// <summary>
/// Define las operaciones completas para la entidad `Usuario`, combinando lectura y escritura.
/// </summary>
/// <remarks>
/// Esta interfaz extiende `IUsuarioReader` y `IUsuarioWriter`, proporcionando acceso 
/// tanto a la consulta como a la modificación de los datos de usuario en el sistema.
/// </remarks>
public interface IUsuarioRepository : IUsuarioReader, IUsuarioWriter { }
