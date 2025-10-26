using EasyMoto.Application.DependencyInjection;
using EasyMoto.Infrastructure.DependencyInjection;
using EasyMoto.Infrastructure.Mongo;
using EasyMoto.Api.ErrorHandling;
using EasyMoto.Api.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using EasyMoto.Api.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    o.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EasyMoto.Api", Version = "v1" });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "EasyMoto.Api", Version = "v2" });

    c.EnableAnnotations();
    c.ExampleFilters();
    c.OperationFilter<EasyMoto.Api.Swagger.ApiVersionHeaderOperationFilter>();
    c.DocumentFilter<EasyMoto.Api.Swagger.SwaggerTagOrderDocumentFilter>();
    c.SupportNonNullableReferenceTypes();
    c.CustomSchemaIds(t => t.FullName);

    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "Informe a chave no header x-api-key",
        Type = SecuritySchemeType.ApiKey,
        Name = "x-api-key",
        In = ParameterLocation.Header
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" } },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddSwaggerExamplesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddApplication();
builder.Services.AddInfrastructureMongo(builder.Configuration);
builder.Services.AddMongo(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", p => p
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed(_ => true)
        .AllowCredentials());
});

builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
});

builder.Services.AddVersionedApiExplorer(o =>
{
    o.GroupNameFormat = "'v'VVV";
    o.SubstituteApiVersionInUrl = false;
});

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = ctx =>
    {
        ctx.ProblemDetails.Instance = ctx.HttpContext.Request.Path;
        ctx.ProblemDetails.Extensions["traceId"] = ctx.HttpContext.TraceIdentifier;
        if (ctx.HttpContext.Request.Headers.TryGetValue("X-Correlation-Id", out var cid))
            ctx.ProblemDetails.Extensions["correlationId"] = cid.ToString();
    };
});
builder.Services.Configure<ApiBehaviorOptions>(options =>
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
        var result = new BadRequestObjectResult(problem);
        result.ContentTypes.Add("application/problem+json");
        return result;
    };
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddHealthChecks().AddCheck<MongoHealthCheck>("mongo");

var app = builder.Build();

app.UseMiddleware<CorrelationIdMiddleware>();
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EasyMoto.Api v1");
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "EasyMoto.Api v2");
});
app.UseCors("frontend");
app.UseHttpsRedirection();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();
