using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations;

public sealed class VagaConfiguration : IEntityTypeConfiguration<Vaga>
{
    public void Configure(EntityTypeBuilder<Vaga> b)
    {
        b.ToTable("vagas");

        b.HasKey(v => v.Id).HasName("pk_vagas");
        b.Property(v => v.Id).HasColumnName("Id").ValueGeneratedOnAdd();

        b.Property(v => v.PatioId).HasColumnName("PatioId").IsRequired();
        b.Property(v => v.NumeroVaga).HasColumnName("NumeroVaga").IsRequired();
        b.Property(v => v.Ocupada).HasColumnName("Ocupada").IsRequired();
        b.Property(v => v.MotoId).HasColumnName("MotoId");

        b.HasIndex(v => new { v.PatioId, v.NumeroVaga })
            .IsUnique()
            .HasDatabaseName("IX_vagas_PatioId_NumeroVaga");

        b.HasOne(v => v.Patio)
            .WithMany(p => p.Vagas)
            .HasForeignKey(v => v.PatioId)
            .HasConstraintName("fk_vagas_patios_patio_id")
            .OnDelete(DeleteBehavior.Cascade);

        b.HasOne(v => v.Moto)
            .WithMany()
            .HasForeignKey(v => v.MotoId)
            .HasConstraintName("fk_vagas_motos_moto_id")
            .OnDelete(DeleteBehavior.SetNull);
    }
}