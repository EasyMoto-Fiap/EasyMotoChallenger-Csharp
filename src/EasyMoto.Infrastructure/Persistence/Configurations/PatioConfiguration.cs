using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations;

public sealed class PatioConfiguration : IEntityTypeConfiguration<Patio>
{
    public void Configure(EntityTypeBuilder<Patio> builder)
    {
        builder.ToTable("patios");
        builder.HasKey(p => p.Id).HasName("pk_patios");
        builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();
        builder.Property(p => p.NomePatio).HasColumnName("NomePatio").HasMaxLength(120).IsRequired(false);
        builder.Property(p => p.TamanhoPatio).HasColumnName("TamanhoPatio").HasMaxLength(60).IsRequired();
        builder.Property(p => p.Andar).HasColumnName("Andar").HasMaxLength(20).IsRequired(false);
        builder.Property(p => p.FilialId).HasColumnName("FilialId").IsRequired();
        builder.HasIndex(p => p.FilialId).HasDatabaseName("IX_patios_FilialId");
        builder.HasOne<Filial>().WithMany().HasForeignKey(p => p.FilialId).HasConstraintName("fk_patios_filiais_filial_id").OnDelete(DeleteBehavior.Cascade);
    }
}