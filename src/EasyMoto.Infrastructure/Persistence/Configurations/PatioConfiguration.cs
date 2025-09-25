using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations
{
    public class PatioConfiguration : IEntityTypeConfiguration<Patio>
    {
        public void Configure(EntityTypeBuilder<Patio> builder)
        {
            builder.ToTable("patios");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.NomePatio).HasColumnName("NomePatio").HasMaxLength(120);
            builder.Property(x => x.TamanhoPatio).HasColumnName("TamanhoPatio").HasMaxLength(60).IsRequired();
            builder.Property(x => x.Andar).HasColumnName("Andar").HasMaxLength(20);
            builder.Property(x => x.FilialId).HasColumnName("FilialId").IsRequired();

            builder.HasIndex(x => x.FilialId);
        }
    }
}