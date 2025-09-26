using EasyMoto.Api.Extensions;
using EasyMoto.Api.Filters;
using EasyMoto.Application.Clientes;
using EasyMoto.Application.Operadores;
using EasyMoto.Domain.Exceptions;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using EasyMoto.Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(o => o.Filters.Add<ValidationFilter>());
builder.Services.AddSwaggerDocumentation();

var cs =
    builder.Configuration.GetConnectionString("Default") ??
    builder.Configuration.GetConnectionString("DefaultConnection") ??
    builder.Configuration["ConnectionStrings:Default"] ??
    builder.Configuration["ConnectionStrings:DefaultConnection"];

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(cs).AddInterceptors()
);

builder.Services.AddScoped<IOperadorRepository, OperadorRepository>();
builder.Services.AddScoped<IVagaRepository, VagaRepository>();
builder.Services.AddScoped<IPatioRepository, PatioRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<IClienteLocacaoRepository, ClienteLocacaoRepository>();
builder.Services.AddScoped<ILocalizacaoRepository, LocalizacaoRepository>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<IFilialRepository, FilialRepository>();
builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();

builder.Services.AddScoped<CriarOperadorHandler>();
builder.Services.AddScoped<AtualizarOperadorHandler>();
builder.Services.AddScoped<ObterOperadorPorIdHandler>();
builder.Services.AddScoped<ListarOperadoresHandler>();
builder.Services.AddScoped<ExcluirOperadorHandler>();

builder.Services.AddScoped<ListarClientesHandler>();
builder.Services.AddScoped<ObterClientePorIdHandler>();
builder.Services.AddScoped<CriarClienteHandler>();
builder.Services.AddScoped<AtualizarClienteHandler>();
builder.Services.AddScoped<ExcluirClienteHandler>();

var app = builder.Build();

app.UseExceptionHandler(b =>
{
    b.Run(async ctx =>
    {
        var ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
        var isValidation = ex is DomainValidationException || ex is ArgumentException;
        ctx.Response.StatusCode = isValidation ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
        ctx.Response.ContentType = "application/problem+json";
        await ctx.Response.WriteAsJsonAsync(new ProblemDetails { Title = isValidation ? "Erro de validação" : "Erro interno", Detail = ex?.Message, Status = ctx.Response.StatusCode });
    });
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

app.UseSwaggerDocumentation();
app.UseAuthorization();
app.MapControllers();
app.Run();
