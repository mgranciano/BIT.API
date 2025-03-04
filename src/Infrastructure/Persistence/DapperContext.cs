using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence;

/// <summary>
/// Representa el contexto de base de datos para Dapper.
/// </summary>
public class DapperContext
{
    /// <summary>
    /// Configuración de la aplicación.
    /// </summary>
    private readonly IConfiguration _configuration;
    /// <summary>
    /// Cadena de conexión a la base de datos.
    /// </summary>
    private readonly string _connectionString;

    /// <summary>
    /// Constructor de DapperContext.
    /// </summary>
    /// <param name="configuration">Interfaz de configuración de la aplicación.</param>
    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }

    /// <summary>
    /// Crea y devuelve una nueva conexión a la base de datos.
    /// </summary>
    /// <returns>Una instancia de IDbConnection con la conexión abierta.</returns>
    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}