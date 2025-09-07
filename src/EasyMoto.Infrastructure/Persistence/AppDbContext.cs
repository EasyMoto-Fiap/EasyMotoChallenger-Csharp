using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Moto> Motos => Set<Moto>();
    public DbSet<Locacao> Locacoes => Set<Locacao>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

}
