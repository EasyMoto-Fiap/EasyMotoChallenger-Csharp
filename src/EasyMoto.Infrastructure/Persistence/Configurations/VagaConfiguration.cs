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
        b.Property(v => v.Id).HasColumnName("id").ValueGeneratedOnAdd();
        b.Property(v => v.PatioId).HasColumnName("patio_id").IsRequired();
        b.Property(v => v.NumeroVaga).HasColumnName("numero_vaga").IsRequired();
        b.Property(v => v.Ocupada).HasColumnName("ocupada").IsRequired();
        b.Property(v => v.MotoId).HasColumnName("moto_id");
        b.HasIndex(v => new { v.PatioId, v.NumeroVaga }).IsUnique().HasDatabaseName("ix_vagas_patio_id_numero_vaga");
        b.HasOne<Patio>().WithMany(p => p.Vagas).HasForeignKey(v => v.PatioId);
    }
}