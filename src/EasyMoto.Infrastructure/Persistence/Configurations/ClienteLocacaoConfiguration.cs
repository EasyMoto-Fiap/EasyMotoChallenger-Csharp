using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations;

public sealed class ClienteLocacaoConfiguration : IEntityTypeConfiguration<ClienteLocacao>
{
    public void Configure(EntityTypeBuilder<ClienteLocacao> builder)
    {
        builder.ToTable("locacoes");
        builder.HasKey(e => e.Id).HasName("pk_locacoes");

        builder.Property(e => e.Id).HasColumnName("id").UseIdentityByDefaultColumn();
        builder.Property(e => e.ClienteId).HasColumnName("cliente_id").IsRequired();
        builder.Property(e => e.MotoId).HasColumnName("moto_id").IsRequired();
        builder.Property(e => e.DataInicio).HasColumnName("data_inicio");
        builder.Property(e => e.DataFim).HasColumnName("data_fim");
        builder.Property(e => e.StatusLocacao).HasColumnName("status_locacao").HasMaxLength(40);

        builder.HasOne(e => e.Cliente)
            .WithMany(c => c.Locacoes)
            .HasForeignKey(e => e.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Moto)
            .WithMany(m => m.Locacoes)
            .HasForeignKey(e => e.MotoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}