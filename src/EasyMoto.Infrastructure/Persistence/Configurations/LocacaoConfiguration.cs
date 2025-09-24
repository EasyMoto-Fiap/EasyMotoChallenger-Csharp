using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations;

public sealed class LocacaoConfiguration : IEntityTypeConfiguration<Locacao>
{
    public void Configure(EntityTypeBuilder<Locacao> builder)
    {
        builder.ToTable("locacoes");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ClienteId).IsRequired();
        builder.Property(x => x.MotoId).IsRequired();

        builder.OwnsOne(x => x.Periodo, p =>
        {
            p.Property(v => v.Inicio).HasColumnName("inicio").IsRequired();
            p.Property(v => v.Fim).HasColumnName("fim").IsRequired();
        });

        builder.HasOne<Cliente>().WithMany().HasForeignKey(x => x.ClienteId);
        builder.HasOne<Moto>().WithMany().HasForeignKey(x => x.MotoId);
    }
}