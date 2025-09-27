using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations

{
    public sealed class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("clientes");

            builder.HasKey(x => x.Id).HasName("pk_clientes");
            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityByDefaultColumn();

            builder.Property(x => x.NomeCliente)
                .HasColumnName("nome_cliente")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.CpfCliente)
                .HasColumnName("cpf_cliente")
                .HasMaxLength(11)
                .IsRequired();

            builder.HasIndex(x => x.CpfCliente)
                .IsUnique()
                .HasDatabaseName("ix_clientes_cpf_cliente");

            builder.Property(x => x.TelefoneCliente)
                .HasColumnName("telefone_cliente")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(x => x.EmailCliente)
                .HasColumnName("email_cliente")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}