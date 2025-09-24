using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using EasyMoto.Infrastructure.Persistence;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Repositories;
using EasyMoto.Application.ClienteLocacoes;
using EasyMoto.Application.Motos;
using EasyMoto.Application.Clientes;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EasyMoto API", Version = "v1" });
});

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<IClienteLocacaoRepository, ClienteLocacaoRepository>();

builder.Services.AddScoped<ListarMotosHandler>();
builder.Services.AddScoped<ObterMotoPorIdHandler>();
builder.Services.AddScoped<AtualizarMotoHandler>();
builder.Services.AddScoped<CriarMotoHandler>();
builder.Services.AddScoped<ExcluirMotoHandler>();

builder.Services.AddScoped<ListarClientesHandler>();
builder.Services.AddScoped<ObterClientePorIdHandler>();
builder.Services.AddScoped<CriarClienteHandler>();
builder.Services.AddScoped<AtualizarClienteHandler>();
builder.Services.AddScoped<ExcluirClienteHandler>();

builder.Services.AddScoped<ListarClienteLocacoesHandler>();
builder.Services.AddScoped<ObterClienteLocacaoPorIdHandler>();
builder.Services.AddScoped<CriarClienteLocacaoHandler>();
builder.Services.AddScoped<AtualizarClienteLocacaoHandler>();
builder.Services.AddScoped<ExcluirClienteLocacaoHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();