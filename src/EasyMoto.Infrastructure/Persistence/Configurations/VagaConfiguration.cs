using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations
{
    public class VagaConfiguration : IEntityTypeConfiguration<Vaga>
    {
        public void Configure(EntityTypeBuilder<Vaga> builder)
        {
            builder.ToTable("vagas");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.NumeroVaga)
                .HasColumnName("NumeroVaga")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.StatusVaga)
                .HasColumnName("StatusVaga")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.MotoId)
                .HasColumnName("MotoId");

            builder.Property(x => x.PatioId)
                .HasColumnName("PatioId")
                .IsRequired();

            builder.HasIndex(x => x.PatioId);
            builder.HasIndex(x => x.MotoId);

            builder.HasIndex(x => new { x.PatioId, x.NumeroVaga })
                .IsUnique();

            builder.HasOne<Moto>()
                .WithMany()
                .HasForeignKey(x => x.MotoId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne<Patio>()
                .WithMany()
                .HasForeignKey(x => x.PatioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}