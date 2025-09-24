using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations
{
    public sealed class MotoConfiguration : IEntityTypeConfiguration<Moto>
    {
        public void Configure(EntityTypeBuilder<Moto> builder)
        {
            builder.ToTable("motos");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Placa)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.Modelo)
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.AnoFabricacao)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.LocacaoId);
            builder.Property(x => x.LocalizacaoId);

            builder.HasIndex(x => x.Placa).IsUnique();
        }
    }
}