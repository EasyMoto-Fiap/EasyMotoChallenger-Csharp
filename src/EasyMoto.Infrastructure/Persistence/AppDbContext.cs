using EasyMoto.Domain.Entities;
using EasyMoto.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Moto> Motos => Set<Moto>();
        public DbSet<ClienteLocacao> ClienteLocacoes => Set<ClienteLocacao>();
        public DbSet<Localizacao> Localizacoes => Set<Localizacao>();
        public DbSet<Empresa> Empresas => Set<Empresa>();
        public DbSet<Filial> Filiais => Set<Filial>();
        public DbSet<Funcionario> Funcionarios => Set<Funcionario>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new MotoConfiguration());
            modelBuilder.ApplyConfiguration(new ClienteLocacaoConfiguration());
            modelBuilder.ApplyConfiguration(new LocalizacaoConfiguration());
            modelBuilder.ApplyConfiguration(new EmpresaConfiguration());
            modelBuilder.ApplyConfiguration(new FilialConfiguration());
            modelBuilder.ApplyConfiguration(new FuncionarioConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}