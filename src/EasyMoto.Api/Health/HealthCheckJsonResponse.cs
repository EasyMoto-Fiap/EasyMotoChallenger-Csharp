using System.Text.Json;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EasyMoto.Api.Health;

public static class HealthCheckJsonResponse
{
    public static Task WriteResponse(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = "application/json; charset=utf-8";
        var payload = new
        {
            status = report.Status.ToString(),
            totalDuration = report.TotalDuration.TotalMilliseconds,
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                duration = e.Value.Duration.TotalMilliseconds,
                error = e.Value.Exception?.Message,
                data = e.Value.Data
            })
        };
        var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        return context.Response.WriteAsync(json);
    }
} 