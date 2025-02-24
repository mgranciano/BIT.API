using Application.Constants;

namespace Application.DTOs;

/// <summary>
/// Representa la estructura estándar de respuesta de los endpoints.
/// </summary>
/// <typeparam name="T">Tipo de datos del objeto de respuesta.</typeparam>
public class ResponseDTO<T>
{
    /// <summary>
    /// Estado de la operación, basado en los valores `S`, `W` o `E`.
    /// </summary>
    public string Estatus { get; set; }

    /// <summary>
    /// Mensaje descriptivo del resultado de la operación.
    /// </summary>
    public string Mensaje { get; set; }

    /// <summary>
    /// Objeto de respuesta, puede ser `null`, un valor simple o un objeto JSON.
    /// </summary>
    public T? ResponseObject { get; set; }

    /// <summary>
    /// Constructor de respuesta estándar sin datos en `ResponseObject`.
    /// </summary>
    public ResponseDTO(string estatus, string mensaje)
    {
        Estatus = estatus;
        Mensaje = mensaje;
        ResponseObject = default;
    }

    /// <summary>
    /// Constructor de respuesta estándar con un objeto en `ResponseObject`.
    /// </summary>
    public ResponseDTO(string estatus, string mensaje, T? responseObject) 
    {
        Estatus = estatus;
        Mensaje = mensaje;
        ResponseObject = responseObject;
    }
    /// <summary>
    /// Método auxiliar para crear una respuesta exitosa.
    /// </summary>
    public static ResponseDTO<T> Exito(string mensaje, T responseObject) =>
        new ResponseDTO<T>(ApiEstatusGenerales.EstatusExito, mensaje, responseObject);

    /// <summary>
    /// Método auxiliar para crear una respuesta de advertencia.
    /// </summary>
    public static ResponseDTO<T> Advertencia(string mensaje) =>
        new ResponseDTO<T>(ApiEstatusGenerales.EstatusAdvertencia, mensaje, default);

    /// <summary>
    /// Método auxiliar para crear una respuesta de error.
    /// </summary>
    public static ResponseDTO<T> Error(string mensaje) =>
        new ResponseDTO<T>(ApiEstatusGenerales.EstatusError, mensaje, default);
}
