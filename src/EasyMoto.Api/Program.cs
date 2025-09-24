using EasyMoto.Application.Clientes;
using EasyMoto.Application.Locacoes;
using EasyMoto.Application.Motos;
using EasyMoto.Infrastructure.Persistence;
using EasyMoto.Infrastructure.Repositories;
using EasyMoto.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(cs));

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<ILocacaoRepository, LocacaoRepository>();

builder.Services.AddScoped<CriarClienteHandler>();
builder.Services.AddScoped<AtualizarClienteHandler>();
builder.Services.AddScoped<ExcluirClienteHandler>();
builder.Services.AddScoped<ObterClientePorIdHandler>();
builder.Services.AddScoped<ListarClientesHandler>();

builder.Services.AddScoped<CriarMotoHandler>();
builder.Services.AddScoped<AtualizarMotoHandler>();
builder.Services.AddScoped<ExcluirMotoHandler>();
builder.Services.AddScoped<ObterMotoPorIdHandler>();
builder.Services.AddScoped<ListarMotosHandler>();

builder.Services.AddScoped<CriarLocacaoHandler>();
builder.Services.AddScoped<AtualizarLocacaoHandler>();
builder.Services.AddScoped<ExcluirLocacaoHandler>();
builder.Services.AddScoped<ObterLocacaoPorIdHandler>();
builder.Services.AddScoped<ListarLocacoesHandler>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();