using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations;

public sealed class MotoConfiguration : IEntityTypeConfiguration<Moto>
{
    public void Configure(EntityTypeBuilder<Moto> b)
    {
        b.ToTable("MOTO");
        b.HasKey(x => x.IdMoto);

        b.Property(x => x.IdMoto)
            .HasColumnName("ID_MOTO")
            .ValueGeneratedOnAdd();

        b.Property(x => x.Modelo)
            .HasColumnName("MODELO")
            .HasMaxLength(120)
            .IsRequired();

        b.Property(x => x.Placa)
            .HasColumnName("PLACA")
            .HasMaxLength(10)
            .IsRequired();

        b.Property(x => x.Ano)
            .HasColumnName("ANO")
            .IsRequired();

        b.Property(x => x.Status)
            .HasColumnName("STATUS")
            .HasConversion<int>()
            .IsRequired();

        b.HasIndex(x => x.Placa).IsUnique();
    }
}
