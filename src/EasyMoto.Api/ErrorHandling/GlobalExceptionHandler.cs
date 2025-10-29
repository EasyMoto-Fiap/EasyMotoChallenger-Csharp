using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.ErrorHandling;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var status = MapStatusCode(exception);
        var title = GetTitle(status);

        var problem = new ProblemDetails
        {
            Type = $"https://httpstatuses.com/{status}",
            Title = title,
            Status = status,
            Detail = BuildDetail(exception, status),
            Instance = httpContext.Request.Path
        };

        problem.Extensions["traceId"] = httpContext.TraceIdentifier;
        if (httpContext.Request.Headers.TryGetValue("X-Correlation-Id", out var cid))
            problem.Extensions["correlationId"] = cid.ToString();

        httpContext.Response.StatusCode = status;
        httpContext.Response.ContentType = "application/problem+json";
        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
        return true;
    }

    static int MapStatusCode(Exception ex)
    {
        var name = ex.GetType().Name;
        var msg = ex.Message ?? string.Empty;

        if (ex is ArgumentException || ex is InvalidDataException || ex is FormatException) return StatusCodes.Status400BadRequest;
        if (ex is KeyNotFoundException) return StatusCodes.Status404NotFound;
        if (ex is UnauthorizedAccessException) return StatusCodes.Status401Unauthorized;
        if (ex is InvalidOperationException) return StatusCodes.Status409Conflict;
        if (ex is TaskCanceledException || ex is OperationCanceledException) return StatusCodes.Status400BadRequest;
        if (name == "MongoWriteException" && (msg.Contains("E11000") || msg.Contains("duplicate key", StringComparison.OrdinalIgnoreCase))) return StatusCodes.Status409Conflict;
        if (name == "MongoConnectionException" || name == "MongoConnectionException`1") return StatusCodes.Status503ServiceUnavailable;
        if (ex is TimeoutException) return StatusCodes.Status408RequestTimeout;
        return StatusCodes.Status500InternalServerError;
    }

    static string GetTitle(int status) =>
        status switch
        {
            StatusCodes.Status400BadRequest => "Bad Request",
            StatusCodes.Status401Unauthorized => "Unauthorized",
            StatusCodes.Status404NotFound => "Not Found",
            StatusCodes.Status408RequestTimeout => "Request Timeout",
            StatusCodes.Status409Conflict => "Conflict",
            StatusCodes.Status503ServiceUnavailable => "Service Unavailable",
            StatusCodes.Status500InternalServerError => "Internal Server Error",
            _ => ((HttpStatusCode)status).ToString()
        };

    static string? BuildDetail(Exception ex, int status) =>
        status >= 500 ? "An unexpected error occurred." : ex.Message;
}
