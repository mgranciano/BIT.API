namespace Application.Interfaces;

/// <summary>
/// Interfaz para el servicio de logging de la aplicación.
/// </summary>
public interface ILogService
{
    void GenerarUID();
    string ObtenerUID();
    void EstablecerEndpoint(string endpoint);
    void Informacion(string origen, string mensaje);
    void Advertencia(string origen, string mensaje);
    void Error(string origen, string mensaje, Exception? ex = null);
    void Correcto(string origen, string mensaje);
}