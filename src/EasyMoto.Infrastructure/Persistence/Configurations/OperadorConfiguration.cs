using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations
{
    public class OperadorConfiguration : IEntityTypeConfiguration<Operador>
    {
        public void Configure(EntityTypeBuilder<Operador> builder)
        {
            builder.ToTable("operadores");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnName("Id").IsRequired();
            builder.Property(o => o.NomeOperador).HasColumnName("NomeOperador").HasMaxLength(120).IsRequired();
            builder.Property(o => o.Cpf).HasColumnName("cpf").HasMaxLength(14).IsRequired();
            builder.Property(o => o.Telefone).HasColumnName("telefone").HasMaxLength(11).IsRequired();
            builder.Property(o => o.Email).HasColumnName("email").HasMaxLength(160).IsRequired();
            builder.Property(o => o.FilialId).HasColumnName("FilialId").IsRequired();
            builder.HasOne(o => o.Filial).WithMany().HasForeignKey(o => o.FilialId);
        }
    }
}