using EasyMoto.Application.Clientes;
using EasyMoto.Application.Locacoes;
using EasyMoto.Application.Motos;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using EasyMoto.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using EasyMoto.Infrastructure.Mongo;
using Asp.Versioning;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using EasyMoto.Api.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Swashbuckle.AspNetCore.Filters;
using EasyMoto.Api.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IClienteRepository, ClienteRepositoryMongo>();
builder.Services.AddScoped<IMotoRepository, MotoRepositoryMongo>();
builder.Services.AddScoped<ILocacaoRepository, LocacaoRepositoryMongo>();

builder.Services.AddScoped<CriarClienteHandler>();
builder.Services.AddScoped<ObterClientePorIdHandler>();
builder.Services.AddScoped<ListarClientesHandler>();
builder.Services.AddScoped<AtualizarClienteHandler>();
builder.Services.AddScoped<ExcluirClienteHandler>();

builder.Services.AddScoped<CriarMotoHandler>();
builder.Services.AddScoped<ObterMotoPorIdHandler>();
builder.Services.AddScoped<ListarMotosHandler>();
builder.Services.AddScoped<AtualizarMotoHandler>();
builder.Services.AddScoped<ExcluirMotoHandler>();

builder.Services.AddScoped<CriarLocacaoHandler>();
builder.Services.AddScoped<ObterLocacaoPorIdHandler>();
builder.Services.AddScoped<ListarLocacoesHandler>();
builder.Services.AddScoped<AtualizarLocacaoHandler>();
builder.Services.AddScoped<ExcluirLocacaoHandler>();

builder.Services.AddControllers();

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("Mongo"));
builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "live" })
    .AddCheck<MongoHealthCheck>("mongo", tags: new[] { "ready" });

builder.Services.AddApiVersioning(o =>
{
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.ReportApiVersions = true;
    o.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddApiExplorer(o =>
{
    o.GroupNameFormat = "'v'VVV";
    o.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "EasyMoto API", Version = "v1", Description = "API do CP4 — v1" });
    opt.SwaggerDoc("v2", new OpenApiInfo { Title = "EasyMoto API", Version = "v2", Description = "API do CP5 — v2" });
    opt.EnableAnnotations();
    opt.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<LocacoesController>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(ui =>
    {
        ui.SwaggerEndpoint("/swagger/v1/swagger.json", "EasyMoto API v1");
        ui.SwaggerEndpoint("/swagger/v2/swagger.json", "EasyMoto API v2");
        ui.RoutePrefix = "swagger";
        ui.DocumentTitle = "EasyMoto API";
        ui.DocExpansion(DocExpansion.None);
        ui.DefaultModelsExpandDepth(-1);
        ui.DisplayRequestDuration();
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = HealthCheckExtensions.WriteResponse
});

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = r => r.Tags.Contains("live"),
    ResponseWriter = HealthCheckExtensions.WriteResponse
});

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = r => r.Tags.Contains("ready"),
    ResponseWriter = HealthCheckExtensions.WriteResponse
});

app.MapGet("/ping", () => Results.Ok("pong"));

app.Run();
