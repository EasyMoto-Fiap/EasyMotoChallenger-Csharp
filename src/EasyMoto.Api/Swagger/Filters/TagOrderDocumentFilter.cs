using System;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EasyMoto.Api.Swagger.Filters;

public class TagOrderDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var desired = new[] { "Clientes", "Motos", "Locacoes", "Health" };

        foreach (var name in desired)
            if (!swaggerDoc.Tags.Any(t => t.Name == name))
                swaggerDoc.Tags.Add(new OpenApiTag { Name = name });

        swaggerDoc.Tags = swaggerDoc.Tags
            .OrderBy(t =>
            {
                var i = Array.IndexOf(desired, t.Name);
                return i < 0 ? int.MaxValue : i;
            })
            .ToList();
    }
}