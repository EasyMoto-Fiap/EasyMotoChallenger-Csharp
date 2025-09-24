using EasyMoto.Infrastructure.Persistence;
using EasyMoto.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using EasyMoto.Application.Clientes;
using EasyMoto.Application.Motos;
using EasyMoto.Application.ClienteLocacoes;
using EasyMoto.Application.Localizacoes;
using EasyMoto.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EasyMoto API", Version = "v1" });
});

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<IClienteLocacaoRepository, ClienteLocacaoRepository>();
builder.Services.AddScoped<ILocalizacaoRepository, LocalizacaoRepository>();

builder.Services.AddScoped<ListarClientesHandler>();
builder.Services.AddScoped<ObterClientePorIdHandler>();
builder.Services.AddScoped<CriarClienteHandler>();
builder.Services.AddScoped<AtualizarClienteHandler>();
builder.Services.AddScoped<ExcluirClienteHandler>();

builder.Services.AddScoped<ListarMotosHandler>();
builder.Services.AddScoped<ObterMotoPorIdHandler>();
builder.Services.AddScoped<CriarMotoHandler>();
builder.Services.AddScoped<AtualizarMotoHandler>();
builder.Services.AddScoped<ExcluirMotoHandler>();

builder.Services.AddScoped<ListarClienteLocacoesHandler>();
builder.Services.AddScoped<ObterClienteLocacaoPorIdHandler>();
builder.Services.AddScoped<CriarClienteLocacaoHandler>();
builder.Services.AddScoped<AtualizarClienteLocacaoHandler>();
builder.Services.AddScoped<ExcluirClienteLocacaoHandler>();

builder.Services.AddScoped<ListarLocalizacoesHandler>();
builder.Services.AddScoped<ObterLocalizacaoPorIdHandler>();
builder.Services.AddScoped<CriarLocalizacaoHandler>();
builder.Services.AddScoped<AtualizarLocalizacaoHandler>();
builder.Services.AddScoped<ExcluirLocalizacaoHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
