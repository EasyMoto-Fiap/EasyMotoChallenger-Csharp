using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations;

public class LocalizacaoConfiguration : IEntityTypeConfiguration<Localizacao>
{
    public void Configure(EntityTypeBuilder<Localizacao> builder)
    {
        builder.ToTable("localizacoes");
        builder.HasKey(e => e.Id).HasName("pk_localizacoes");
        builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(e => e.StatusLoc).HasColumnName("status_loc").HasMaxLength(40);
        builder.Property(e => e.DataHora).HasColumnName("data_hora");
        builder.Property(e => e.ZonaVirtual).HasColumnName("zona_virtual").HasMaxLength(80);
        builder.Property(e => e.Latitude).HasColumnName("latitude");
        builder.Property(e => e.Longitude).HasColumnName("longitude");
    }
}