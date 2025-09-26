using EasyMoto.Api.Extensions;
using EasyMoto.Api.Filters;
using EasyMoto.Application.Clientes;
using EasyMoto.Application.Operadores;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using EasyMoto.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(o => o.Filters.Add<ValidationFilter>());
builder.Services.AddSwaggerDocumentation();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var cs = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(cs);
});

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

app.UseSwaggerDocumentation();
app.UseAuthorization();
app.MapControllers();
app.Run();
