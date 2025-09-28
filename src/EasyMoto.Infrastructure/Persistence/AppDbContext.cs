using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Filial> Filiais => Set<Filial>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Moto> Motos => Set<Moto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}