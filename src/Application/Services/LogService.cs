namespace Infrastructure.Services;

using Application.Interfaces;
using Serilog;
using System;
using System.Threading;

/// <summary>
/// Servicio centralizado de logging con Serilog.
/// </summary>
public class LogService : ILogService
{
    private readonly AsyncLocal<string?> _uid = new AsyncLocal<string?>();
    private readonly AsyncLocal<string?> _endpoint = new AsyncLocal<string?>();
    /// <summary>
    /// Genera y almacena un UID único por solicitud.
    /// </summary>
    public void GenerarUID()
    {
        _uid.Value = Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Obtiene el UID actual para la solicitud.
    /// </summary>
    public string ObtenerUID()
    {
        return _uid.Value ?? "SIN-UID";
    }
        
    /// <summary>
    /// Almacena la ruta del endpoint accedido en la solicitud actual.
    /// </summary>
    /// <param name="endpoint">Ruta del endpoint (ejemplo: "/api/usuarios/obtenerUsuarios").</param>
    public void EstablecerEndpoint(string endpoint)
    {
        _endpoint.Value = endpoint;
    }

    /// <summary>
    /// Registra un mensaje de información con el origen y UID.
    /// </summary>
    public void Informacion(string origen, string mensaje)
    {
        Log.Information("📖 [{UID}] [{Origen}] [{Endpoint}] [INFORMATION] {Mensaje}", ObtenerUID(), origen,_endpoint.Value ?? "SIN-ENDPOINT",  mensaje);
    }

    /// <summary>
    /// Registra una advertencia con el origen y UID.
    /// </summary>
    public void Advertencia(string origen, string mensaje)
    {
        Log.Warning("⚠️ [{UID}] [{Origen}] [{Endpoint}] [WARNING] {Mensaje}", ObtenerUID(), origen, _endpoint.Value ?? "SIN-ENDPOINT", mensaje);
    }

    /// <summary>
    /// Registra un error con el origen y UID.
    /// </summary>
    public void Error(string origen, string mensaje, Exception? ex = null)
    {
        if (ex != null)
            Log.Error(ex, "🚨 [{UID}] [{Origen}] [{Endpoint}] [ERROR] {Mensaje}", ObtenerUID(), origen, _endpoint.Value ?? "SIN-ENDPOINT", mensaje);
        else
            Log.Error("🚨 [{UID}] [{Origen}] [{Endpoint}] [ERROR] {Mensaje}", ObtenerUID(), origen, _endpoint.Value ?? "SIN-ENDPOINT", mensaje);
    }

     /// <summary>
    /// Registra un evento terminado correctamente con el origen y UID.
    /// </summary>
    public void Correcto(string origen, string mensaje)
    {
        Log.Information("✅ [{UID}] [{Origen}] [{Endpoint}] [SUCCESS] {Mensaje}", ObtenerUID(), origen, _endpoint.Value ?? "SIN-ENDPOINT", mensaje);
    }
}
