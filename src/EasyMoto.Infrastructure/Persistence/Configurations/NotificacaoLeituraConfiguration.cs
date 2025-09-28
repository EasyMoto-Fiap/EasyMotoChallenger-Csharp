using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations
{
    public class NotificacaoLeituraConfiguration : IEntityTypeConfiguration<NotificacaoLeitura>
    {
        public void Configure(EntityTypeBuilder<NotificacaoLeitura> builder)
        {
            builder.ToTable("notificacao_leitura");
            builder.HasKey(x => new { x.NotificacaoId, x.UsuarioId }).HasName("PK_notificacao_leitura");
            builder.Property(x => x.NotificacaoId).HasColumnName("notificacao_id");
            builder.Property(x => x.UsuarioId).HasColumnName("usuario_id");
            builder.Property(x => x.LidoEm).HasColumnName("lido_em").IsRequired();
        }
    }
}