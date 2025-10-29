using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EasyMoto.Api.Swagger;

public sealed class SwaggerTagOrderDocumentFilter : IDocumentFilter
{
    private static readonly string[] DesiredOrder = new[]
    {
        "Auth",
        "Health",
        "Filial",
        "Usuário",
        "LegendaStatus",
        "Moto",
        "Notificacao"
    };

    private static readonly Dictionary<string, string> TagMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Auth"] = "Auth",
        ["Authentication"] = "Auth",
        ["Health"] = "Health",
        ["Healthz"] = "Health",
        ["Filial"] = "Filial",
        ["Filiais"] = "Filial",
        ["Usuario"] = "Usuário",
        ["Usuarios"] = "Usuário",
        ["Usuário"] = "Usuário",
        ["LegendaStatus"] = "LegendaStatus",
        ["LegendasStatus"] = "LegendaStatus",
        ["Moto"] = "Moto",
        ["Motos"] = "Moto",
        ["Notificacao"] = "Notificacao",
        ["Notificacoes"] = "Notificacao",
        ["Notificação"] = "Notificacao"
    };

    private static readonly Dictionary<string, string> TagDescriptions = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Auth"] = "Autenticação e login.",
        ["Health"] = "Saúde da aplicação e dependências.",
        ["Filial"] = "Gestão de filiais.",
        ["Usuário"] = "Gestão de usuários.",
        ["LegendaStatus"] = "Catálogo de legendas/status.",
        ["Moto"] = "Recursos de motos.",
        ["Notificacao"] = "Envio e consulta de notificações."
    };

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var (_, item) in swaggerDoc.Paths)
        {
            foreach (var op in item.Operations.Values)
            {
                if (op.Tags is { Count: > 0 })
                {
                    var original = op.Tags[0].Name;
                    if (TagMap.TryGetValue(original, out var desired))
                        op.Tags[0].Name = desired;
                }
            }
        }

        var ordered = DesiredOrder
            .Select(name => new OpenApiTag
            {
                Name = name,
                Description = TagDescriptions.TryGetValue(name, out var d) ? d : null
            })
            .ToList();

        var remaining = swaggerDoc.Tags?
            .Where(t => DesiredOrder.All(x => !string.Equals(x, t.Name, StringComparison.OrdinalIgnoreCase)))
            .ToList() ?? new List<OpenApiTag>();

        swaggerDoc.Tags = ordered.Concat(remaining).ToList();
    }
}
