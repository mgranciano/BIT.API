using System;

namespace API.Attributes;

/// <summary>
/// Decorador para definir respuestas estándar en Swagger para operaciones de usuario.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class ProducesUsuarioResponseAttribute : Attribute
{
    public Type ResponseType { get; }
    public string Summary { get; }
    public string Description { get; }

    /// <summary>
    /// Constructor del decorador para definir respuestas estándar en Swagger.
    /// </summary>
    /// <param name="responseType">Tipo de DTO que se devolverá en la respuesta.</param>
    /// <param name="summary">Resumen de la operación.</param>
    /// <param name="description">Descripción detallada de la operación.</param>
    public ProducesUsuarioResponseAttribute(Type responseType, string summary, string description)
    {
        ResponseType = responseType;
        Summary = summary;
        Description = description;
    }
}
