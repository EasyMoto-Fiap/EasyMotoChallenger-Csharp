using EasyMoto.Application.Clientes;
using EasyMoto.Application.Locacoes;
using EasyMoto.Application.Motos;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using EasyMoto.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Oracle.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<ILocacaoRepository, LocacaoRepository>();

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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EasyMoto API",
        Version = "v1",
        Description = "cp4 - fiap"
    });
    opt.EnableAnnotations();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Tem que vir ANTES do UseSwaggerUI
    app.UseSwaggerUI(ui =>
    {
        ui.SwaggerEndpoint("/swagger/v1/swagger.json", "EasyMoto API v1");
        ui.DocumentTitle = "EasyMoto API";
        ui.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
