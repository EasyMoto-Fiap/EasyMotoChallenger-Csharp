using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations;

public sealed class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.ToTable("empresas");
        builder.HasKey(x => x.IdEmpresa);

        builder.Property(x => x.NomeEmpresa)
            .HasMaxLength(160)
            .IsRequired();

        builder.Property(x => x.Cnpj)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(x => x.Cnpj).IsUnique();
    }
}