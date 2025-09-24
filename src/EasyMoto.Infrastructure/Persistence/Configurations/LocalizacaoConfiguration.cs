using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations
{
    public class LocalizacaoConfiguration : IEntityTypeConfiguration<Localizacao>
    {
        public void Configure(EntityTypeBuilder<Localizacao> builder)
        {
            builder.ToTable("localizacoes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id").IsRequired();
            builder.Property(x => x.StatusLoc).HasColumnName("status_loc").HasMaxLength(40);
            builder.Property(x => x.DataHora).HasColumnName("data_hora");
            builder.Property(x => x.ZonaVirtual).HasColumnName("zona_virtual").HasMaxLength(80);
            builder.Property(x => x.Latitude).HasColumnName("latitude");
            builder.Property(x => x.Longitude).HasColumnName("longitude");
        }
    }
}