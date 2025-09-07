using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations;

public sealed class LocacaoConfiguration : IEntityTypeConfiguration<Locacao>
{
    public void Configure(EntityTypeBuilder<Locacao> b)
    {
        b.ToTable("CLIENTE_LOCACAO");
        b.HasKey(x => x.IdLocacao);

        b.Property(x => x.IdLocacao)
            .HasColumnName("ID_LOCACAO")
            .ValueGeneratedOnAdd();

        b.Property(x => x.ClienteId)
            .HasColumnName("CLIENTE_ID")
            .IsRequired();

        b.OwnsOne(x => x.Periodo, p =>
        {
            p.Property(pp => pp.Inicio)
                .HasColumnName("DATA_INICIO")
                .IsRequired();

            p.Property(pp => pp.Fim)
                .HasColumnName("DATA_FIM")
                .IsRequired();
        });

        b.Property(x => x.StatusLocacao)
            .HasColumnName("STATUS_LOCACAO")
            .IsRequired();
    }
}
