using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations
{
    public class NotificacaoConfiguration : IEntityTypeConfiguration<Notificacao>
    {
        public void Configure(EntityTypeBuilder<Notificacao> builder)
        {
            builder.ToTable("notificacao");
            builder.HasKey(x => x.Id).HasName("PK_notificacao");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Tipo).HasColumnName("tipo").HasConversion<int>().IsRequired();
            builder.Property(x => x.Mensagem).HasColumnName("mensagem").HasMaxLength(280).IsRequired();
            builder.Property(x => x.MotoId).HasColumnName("moto_id");
            builder.Property(x => x.UsuarioOrigemId).HasColumnName("usuario_origem_id").IsRequired();
            builder.Property(x => x.CriadaEm).HasColumnName("criada_em").IsRequired();
            builder.Property(x => x.Escopo).HasColumnName("escopo").HasConversion<int>().IsRequired();
            builder.HasMany(x => x.Leituras).WithOne().HasForeignKey(l => l.NotificacaoId);
        }
    }
}