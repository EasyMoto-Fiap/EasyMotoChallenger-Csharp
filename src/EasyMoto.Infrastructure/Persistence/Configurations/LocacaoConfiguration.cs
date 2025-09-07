using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations;

public sealed class LocacaoConfiguration : IEntityTypeConfiguration<Locacao>
{
    public void Configure(EntityTypeBuilder<Locacao> b)
    {
        b.ToTable("LOCACAO");
        b.HasKey(x => x.IdLocacao);

        b.Property(x => x.IdLocacao)
            .HasColumnName("ID_LOCACAO")
            .ValueGeneratedOnAdd();

        b.Property(x => x.ClienteId)
            .HasColumnName("CLIENTE_ID")
            .IsRequired();

        b.Property(x => x.MotoId)
            .HasColumnName("MOTO_ID")
            .IsRequired();

        b.OwnsOne(x => x.Periodo, p =>
        {
            p.Property(pp => pp.Inicio)
                .HasColumnName("INICIO")
                .IsRequired();

            p.Property(pp => pp.Fim)
                .HasColumnName("FIM")
                .IsRequired();
        });

        b.Property(x => x.ValorDiaria)
            .HasColumnName("VALOR_DIARIA")
            .HasPrecision(18, 2)
            .IsRequired();

        b.Property(x => x.ValorTotal)
            .HasColumnName("VALOR_TOTAL")
            .HasPrecision(18, 2)
            .IsRequired();

        b.Property(x => x.Status)
            .HasColumnName("STATUS")
            .HasConversion<int>()
            .IsRequired();

        b.Property(x => x.CriadoEm)
            .HasColumnName("CRIADO_EM")
            .IsRequired();

        b.Property(x => x.EncerradoEm)
            .HasColumnName("ENCERRADO_EM");
    }
}
