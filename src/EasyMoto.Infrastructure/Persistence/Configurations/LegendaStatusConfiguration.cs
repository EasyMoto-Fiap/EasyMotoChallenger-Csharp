using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations
{
    public class LegendaStatusConfiguration : IEntityTypeConfiguration<LegendaStatus>
    {
        public void Configure(EntityTypeBuilder<LegendaStatus> builder)
        {
            builder.ToTable("legenda_status");
            builder.HasKey(x => x.Id).HasName("PK_legenda_status");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Titulo).HasColumnName("titulo").HasMaxLength(80).IsRequired();
            builder.Property(x => x.Descricao).HasColumnName("descricao").HasMaxLength(255);
            builder.Property(x => x.CorHex).HasColumnName("cor_hex").HasMaxLength(7);
            builder.Property(x => x.Ativo).HasColumnName("ativo").IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired();
        }
    }
}