using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EasyMoto.Api.Swagger;

public sealed class ApiVersionHeaderOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        if (operation.Parameters.Any(p =>
                p.In == ParameterLocation.Header &&
                string.Equals(p.Name, "x-api-version", StringComparison.OrdinalIgnoreCase)))
        {
            return;
        }

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "x-api-version",
            In = ParameterLocation.Header,
            Required = false,
            Description = "Versão da API. Se não for informado, usa 1.0.",
            Schema = new OpenApiSchema
            {
                Type = "string",
                Default = new OpenApiString("1.0"),
                Example = new OpenApiString("1.0")
            }
        });
    }
}