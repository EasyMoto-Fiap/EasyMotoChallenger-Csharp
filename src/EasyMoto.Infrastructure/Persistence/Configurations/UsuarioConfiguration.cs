using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuarios");

            builder.HasKey(x => x.Id).HasName("PK_usuarios");
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.NomeCompleto)
                .HasColumnName("nome_completo")
                .HasMaxLength(160)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("email")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.SenhaHash)
                .HasColumnName("senha_hash")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Cpf)
                .HasColumnName("cpf")
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(x => x.Telefone)
                .HasColumnName("telefone")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.CepFilial)
                .HasColumnName("cep_filial")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.Perfil)
                .HasColumnName("perfil")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(x => x.Ativo)
                .HasColumnName("ativo")
                .IsRequired();
        }
    }
}