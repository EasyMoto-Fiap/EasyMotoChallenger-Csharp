using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.ErrorHandling;

public static class ValidationProblemDetailsSetup
{
    public static void AddValidationProblemDetails(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var problem = new ValidationProblemDetails(context.ModelState)
                {
                    Type = "https://www.rfc-editor.org/rfc/rfc7807",
                    Title = "Validation error",
                    Status = StatusCodes.Status400BadRequest,
                    Instance = context.HttpContext.Request.Path
                };
                problem.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
                if (context.HttpContext.Request.Headers.TryGetValue("X-Correlation-Id", out var cid))
                    problem.Extensions["correlationId"] = cid.ToString();
                return new BadRequestObjectResult(problem) { ContentTypes = { "application/problem+json" } };
            };
        });
    }
}