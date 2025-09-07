using EasyMoto.Application.Clientes;
using EasyMoto.Infrastructure.Persistence;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using EasyMoto.Application.Motos;
using Oracle.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<CriarClienteHandler>();
builder.Services.AddScoped<ObterClientePorIdHandler>();
builder.Services.AddScoped<ListarClientesHandler>();
builder.Services.AddScoped<AtualizarClienteHandler>();
builder.Services.AddScoped<ExcluirClienteHandler>();

builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<CriarMotoHandler>();
builder.Services.AddScoped<ObterMotoPorIdHandler>();
builder.Services.AddScoped<ListarMotosHandler>();
builder.Services.AddScoped<AtualizarMotoHandler>();
builder.Services.AddScoped<ExcluirMotoHandler>();

builder.Services.AddScoped<ILocacaoRepository, LocacaoRepository>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
