using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations
{
    public sealed class ClienteLocacaoConfiguration : IEntityTypeConfiguration<ClienteLocacao>
    {
        public void Configure(EntityTypeBuilder<ClienteLocacao> builder)
        {
            builder.ToTable("locacoes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ClienteId).IsRequired();
            builder.Property(x => x.MotoId).IsRequired();

            builder.OwnsOne(x => x.Periodo, b =>
            {
                b.Property(p => p.Inicio).HasColumnName("inicio").IsRequired();
                b.Property(p => p.Fim).HasColumnName("fim").IsRequired();
            });

            builder.Property(x => x.StatusLocacao)
                .HasColumnName("status")
                .HasMaxLength(20)
                .HasDefaultValue("Ativa")
                .IsRequired();
        }
    }
}