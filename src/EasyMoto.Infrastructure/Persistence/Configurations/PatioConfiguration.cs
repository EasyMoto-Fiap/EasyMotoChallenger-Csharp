using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations
{
    public sealed class PatioConfiguration : IEntityTypeConfiguration<Patio>
    {
        public void Configure(EntityTypeBuilder<Patio> b)
        {
            b.ToTable("patios");
            b.HasKey(p => p.IdPatio).HasName("pk_patios");
            b.Property(p => p.IdPatio).HasColumnName("IdPatio").ValueGeneratedOnAdd();
            b.Property(p => p.NomePatio).HasColumnName("NomePatio").HasMaxLength(160).IsRequired();
            b.Property(p => p.TamanhoPatio).HasColumnName("TamanhoPatio").HasMaxLength(80).IsRequired();
            b.Property(p => p.Andar).HasColumnName("Andar").HasMaxLength(40).IsRequired();
            b.Property(p => p.FilialId).HasColumnName("FilialId").IsRequired();
            b.HasIndex(p => p.FilialId).HasDatabaseName("IX_patios_FilialId");
            b.HasOne<Filial>().WithMany().HasForeignKey(p => p.FilialId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}