using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations;

public sealed class MotoConfiguration : IEntityTypeConfiguration<Moto>
{
    public void Configure(EntityTypeBuilder<Moto> b)
    {
        b.ToTable("motos");

        b.HasKey(m => m.Id).HasName("pk_motos");
        b.Property(m => m.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd();

        b.Property(m => m.Placa)
            .HasColumnName("Placa")
            .HasMaxLength(10)
            .IsRequired();

        b.Property(m => m.Marca).HasColumnName("Marca").HasMaxLength(80).IsRequired();
        b.Property(m => m.Modelo).HasColumnName("Modelo").HasMaxLength(120).IsRequired();
        b.Property(m => m.Cor).HasColumnName("Cor").HasMaxLength(40).IsRequired();
        b.Property(m => m.AnoFabricacao).HasColumnName("AnoFabricacao").IsRequired();
        b.Property(m => m.Status).HasColumnName("Status").HasMaxLength(40).IsRequired();

        b.Property(m => m.LocacaoId).HasColumnName("LocacaoId");
        b.Property(m => m.LocalizacaoId).HasColumnName("LocalizacaoId").IsRequired();

        b.HasIndex(m => m.Placa).IsUnique().HasDatabaseName("IX_motos_Placa");
    }
}