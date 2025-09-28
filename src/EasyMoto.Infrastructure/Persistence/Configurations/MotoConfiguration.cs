using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations
{
    public class MotoConfiguration : IEntityTypeConfiguration<Moto>
    {
        public void Configure(EntityTypeBuilder<Moto> builder)
        {
            builder.ToTable("moto");
            builder.HasKey(x => x.Id).HasName("PK_moto");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Placa).HasColumnName("placa").HasMaxLength(10).IsRequired();
            builder.Property(x => x.Modelo).HasColumnName("modelo").HasMaxLength(120).IsRequired();
            builder.Property(x => x.Ano).HasColumnName("ano").IsRequired();
            builder.Property(x => x.Cor).HasColumnName("cor").HasMaxLength(40).IsRequired();
            builder.Property(x => x.Ativo).HasColumnName("ativo").IsRequired();
            builder.Property(x => x.FilialId).HasColumnName("filial_id").IsRequired();
            builder.Property(x => x.Categoria).HasColumnName("categoria").HasConversion<int>().IsRequired();
            builder.Property(x => x.StatusOperacional).HasColumnName("status_operacional").HasConversion<int>().IsRequired();
            builder.Property(x => x.LegendaStatusId).HasColumnName("legenda_status_id");
            builder.Property(x => x.QrCode).HasColumnName("qrcode").HasMaxLength(200);
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired();
        }
    }
}