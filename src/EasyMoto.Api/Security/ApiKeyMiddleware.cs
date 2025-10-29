using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;

namespace EasyMoto.Api.Security;

public sealed class ApiKeyMiddleware
{
    const string HeaderName = "x-api-key";
    readonly RequestDelegate _next;
    readonly string _expected;

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration cfg)
    {
        _next = next;
        _expected = cfg["Auth:ApiKey"] ?? string.Empty;
    }

    public async Task Invoke(HttpContext ctx)
    {
        var ep = ctx.GetEndpoint();
        if (ep?.Metadata?.GetMetadata<IAllowAnonymous>() is not null)
        {
            await _next(ctx);
            return;
        }

        var path = ctx.Request.Path.Value ?? string.Empty;
        if (path.StartsWith("/swagger") || path.StartsWith("/health"))
        {
            await _next(ctx);
            return;
        }

        if (string.IsNullOrEmpty(_expected))
        {
            ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await ctx.Response.WriteAsJsonAsync(new
            {
                type = "https://httpstatuses.com/500",
                title = "Internal Server Error",
                status = 500,
                detail = "API key not configured.",
                instance = ctx.Request.Path
            });
            return;
        }

        if (!ctx.Request.Headers.TryGetValue(HeaderName, out StringValues provided) ||
            provided.Count == 0 ||
            !string.Equals(provided[0], _expected, StringComparison.Ordinal))
        {
            ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
            ctx.Response.Headers["WWW-Authenticate"] = "ApiKey";
            await ctx.Response.WriteAsJsonAsync(new
            {
                type = "https://httpstatuses.com/401",
                title = "Unauthorized",
                status = 401,
                detail = "Missing or invalid API key.",
                instance = ctx.Request.Path
            });
            return;
        }

        await _next(ctx);
    }
}
