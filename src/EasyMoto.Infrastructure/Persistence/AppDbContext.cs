using EasyMoto.Domain.Entities;
using EasyMoto.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Moto> Motos => Set<Moto>();
        public DbSet<ClienteLocacao> Locacoes => Set<ClienteLocacao>();
        public DbSet<Localizacao> Localizacoes => Set<Localizacao>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new MotoConfiguration());
            modelBuilder.ApplyConfiguration(new ClienteLocacaoConfiguration());
            modelBuilder.ApplyConfiguration(new LocalizacaoConfiguration());
        }
    }
}