using Microsoft.EntityFrameworkCore;
using EasyMoto.src.Domain.Entities;

namespace EasyMoto.src.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Filial> Filiais { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Operador> Operadores { get; set; }
        public DbSet<Patio> Patios { get; set; }
        public DbSet<ClienteLocacao> ClienteLocacoes { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; }
        public DbSet<Moto> Motos { get; set; }
        public DbSet<Vaga> Vagas { get; set; }
    }
}
