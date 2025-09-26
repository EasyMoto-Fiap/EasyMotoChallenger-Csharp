using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations
{
    public class OperadorConfiguration : IEntityTypeConfiguration<Operador>
    {
        public void Configure(EntityTypeBuilder<Operador> builder)
        {
            builder.ToTable("operador");
            builder.HasKey(x => x.IdOperador);
            builder.Property(x => x.IdOperador).HasColumnName("id_operador");
            builder.Property(x => x.NomeOperador).HasColumnName("nome_opr").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Cpf).HasColumnName("cpf_opr").HasMaxLength(11).IsRequired();
            builder.HasIndex(x => x.Cpf).IsUnique();
            builder.Property(x => x.Telefone).HasColumnName("telefone_opr").HasMaxLength(14).IsRequired();
            builder.Property(x => x.Email).HasColumnName("email_opr").HasMaxLength(100).IsRequired();
            builder.Property(x => x.FilialId).HasColumnName("filial_id").IsRequired();
            builder.HasOne(x => x.Filial).WithMany().HasForeignKey(x => x.FilialId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}