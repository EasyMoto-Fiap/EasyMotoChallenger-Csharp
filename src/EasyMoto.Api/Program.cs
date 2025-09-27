using EasyMoto.Api.Extensions;
using EasyMoto.Api.Filters;
using EasyMoto.Application.ClienteLocacoes;
using EasyMoto.Application.Clientes;
using EasyMoto.Application.Empresas;
using EasyMoto.Application.Filiais;
using EasyMoto.Application.Funcionarios;
using EasyMoto.Application.Localizacoes;
using EasyMoto.Application.Motos;
using EasyMoto.Application.Operadores;
using EasyMoto.Application.Patios;
using EasyMoto.Application.Vagas;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using EasyMoto.Infrastructure.Repositories;
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
{
    options.UseNpgsql(cs).UseSnakeCaseNamingConvention();
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

builder.Services.AddScoped<CriarClienteHandler>();
builder.Services.AddScoped<AtualizarClienteHandler>();
builder.Services.AddScoped<ObterClientePorIdHandler>();
builder.Services.AddScoped<ListarClientesHandler>();
builder.Services.AddScoped<ExcluirClienteHandler>();

builder.Services.AddScoped<CriarEmpresaHandler>();
builder.Services.AddScoped<AtualizarEmpresaHandler>();
builder.Services.AddScoped<ObterEmpresaPorIdHandler>();
builder.Services.AddScoped<ListarEmpresasHandler>();
builder.Services.AddScoped<ExcluirEmpresaHandler>();

builder.Services.AddScoped<CriarFilialHandler>();
builder.Services.AddScoped<AtualizarFilialHandler>();
builder.Services.AddScoped<ObterFilialPorIdHandler>();
builder.Services.AddScoped<ListarFiliaisHandler>();
builder.Services.AddScoped<ExcluirFilialHandler>();

builder.Services.AddScoped<CriarFuncionarioHandler>();
builder.Services.AddScoped<AtualizarFuncionarioHandler>();
builder.Services.AddScoped<ObterFuncionarioPorIdHandler>();
builder.Services.AddScoped<ListarFuncionariosHandler>();
builder.Services.AddScoped<ExcluirFuncionarioHandler>();

builder.Services.AddScoped<CriarLocalizacaoHandler>();
builder.Services.AddScoped<AtualizarLocalizacaoHandler>();
builder.Services.AddScoped<ObterLocalizacaoPorIdHandler>();
builder.Services.AddScoped<ListarLocalizacoesHandler>();
builder.Services.AddScoped<ExcluirLocalizacaoHandler>();

builder.Services.AddScoped<CriarMotoHandler>();
builder.Services.AddScoped<AtualizarMotoHandler>();
builder.Services.AddScoped<ObterMotoPorIdHandler>();
builder.Services.AddScoped<ListarMotosHandler>();
builder.Services.AddScoped<ExcluirMotoHandler>();

builder.Services.AddScoped<CriarClienteLocacaoHandler>();
builder.Services.AddScoped<AtualizarClienteLocacaoHandler>();
builder.Services.AddScoped<ObterClienteLocacaoPorIdHandler>();
builder.Services.AddScoped<ListarClienteLocacoesHandler>();
builder.Services.AddScoped<ExcluirClienteLocacaoHandler>();

builder.Services.AddScoped<CriarPatioHandler>();
builder.Services.AddScoped<AtualizarPatioHandler>();
builder.Services.AddScoped<ObterPatioPorIdHandler>();
builder.Services.AddScoped<ListarPatiosHandler>();
builder.Services.AddScoped<ExcluirPatioHandler>();

builder.Services.AddScoped<CriarVagaHandler>();
builder.Services.AddScoped<AtualizarVagaHandler>();
builder.Services.AddScoped<ObterVagaPorIdHandler>();
builder.Services.AddScoped<ListarVagasHandler>();
builder.Services.AddScoped<ExcluirVagaHandler>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogCritical(ex, "Falha ao aplicar migrations");
        throw;
    }
}

app.UseSwaggerDocumentation();
app.UseAuthorization();
app.MapControllers();
app.Run();
