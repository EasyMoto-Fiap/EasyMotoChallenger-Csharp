using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations;

public sealed class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> b)
    {
        b.ToTable("CLIENTE");
        b.HasKey(x => x.IdCliente);

        b.Property(x => x.IdCliente)
            .HasColumnName("ID_CLIENTE")
            .ValueGeneratedOnAdd();

        b.Property(x => x.NomeCliente)
            .HasColumnName("NOME_CLIENTE")
            .IsRequired();

        b.Property(x => x.CpfCliente)
            .HasColumnName("CPF_CLIENTE")
            .IsRequired();

        b.Property(x => x.TelefoneCliente)
            .HasColumnName("TELEFONE_CLIENTE")
            .IsRequired();

        b.Property(x => x.EmailCliente)
            .HasColumnName("EMAIL_CLIENTE")
            .IsRequired();

        b.HasIndex(x => x.CpfCliente).IsUnique();
    }
}
