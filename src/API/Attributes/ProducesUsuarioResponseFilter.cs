using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Application.DTOs;

namespace API.Attributes;

/// <summary>
/// Filtro de Swagger para aplicar `ProducesUsuarioResponseAttribute` automáticamente.
/// </summary>
public class ProducesUsuarioResponseFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var attribute = context.MethodInfo.GetCustomAttributes(typeof(ProducesUsuarioResponseAttribute), false)
                                          .FirstOrDefault() as ProducesUsuarioResponseAttribute;

        if (attribute == null)
            return;

        if (operation.Responses == null)
            operation.Responses = new OpenApiResponses();

        
        operation.Summary = attribute.Summary;
        operation.Description = attribute.Description;

        operation.Responses["200"] = new OpenApiResponse
        {
            Description = "Éxito al momento de invocar el endpoint",
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = context.SchemaGenerator.GenerateSchema(attribute.ResponseType, context.SchemaRepository)
                }
            }
        };

        operation.Responses["404"] = new OpenApiResponse
        {
            Description = "Hubo algún problema que no detiene la operación: Not Found",
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = context.SchemaGenerator.GenerateSchema(typeof(ResponseDTO<object>), context.SchemaRepository)
                }
            }
        };

        operation.Responses["400"] = new OpenApiResponse
        {
            Description = "Hubo algún problema que no detiene la operación: Bad Request",
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = context.SchemaGenerator.GenerateSchema(typeof(ResponseDTO<object>), context.SchemaRepository)
                }
            }
        };
    }
}
