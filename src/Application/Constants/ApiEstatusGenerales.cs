namespace Application.Constants;

/// <summary>
/// Clase que contiene constantes globales para la API.
/// </summary>
public static class ApiEstatusGenerales
{
    /// <summary>
    /// Éxito al momento de invocar el endpoint.
    /// </summary>
    public const string EstatusExito = "S";

    /// <summary>
    /// Hubo algún problema que no detiene la operación.
    /// </summary>
    public const string EstatusAdvertencia = "W";

    /// <summary>
    /// Hubo un problema que detiene la operación.
    /// </summary>
    public const string EstatusError = "E";
}
