using EasyMoto.Api.Filters;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using EasyMoto.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(o => o.Filters.Add<ValidationFilter>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var cs = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(cs);
});

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<IClienteLocacaoRepository, ClienteLocacaoRepository>(); // <- ajuste aqui
builder.Services.AddScoped<ILocalizacaoRepository, LocalizacaoRepository>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<IFilialRepository, FilialRepository>();
builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();