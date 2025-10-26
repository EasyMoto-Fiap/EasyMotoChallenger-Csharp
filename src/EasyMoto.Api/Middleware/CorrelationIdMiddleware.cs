namespace EasyMoto.Api.Middleware;

public sealed class CorrelationIdMiddleware
{
    readonly RequestDelegate _next;
    public CorrelationIdMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("X-Correlation-Id", out var cid) || string.IsNullOrWhiteSpace(cid))
        {
            cid = Guid.NewGuid().ToString("N");
            context.Request.Headers["X-Correlation-Id"] = cid;
        }
        context.Response.Headers["X-Correlation-Id"] = cid.ToString();
        await _next(context);
    }
}