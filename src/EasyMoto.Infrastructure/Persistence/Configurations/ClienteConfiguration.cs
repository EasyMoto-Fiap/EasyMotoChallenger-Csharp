using EasyMoto.Domain.Entities;
using EasyMoto.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> b)
        {
            b.ToTable("clientes");
            b.HasKey(x => x.Id);

            b.Property(x => x.Nome)
                .HasColumnName("nome_cliente")
                .HasMaxLength(100);

            b.Property(x => x.Cpf)
                .HasConversion(ValueObjectConverters.CpfConverter)
                .HasColumnName("cpf_cliente")
                .HasMaxLength(11)
                .IsRequired();

            b.Property(x => x.Telefone)
                .HasConversion(ValueObjectConverters.TelefoneConverter)
                .HasColumnName("telefone_cliente")
                .HasMaxLength(15);

            b.Property(x => x.Email)
                .HasConversion(ValueObjectConverters.EmailConverter)
                .HasColumnName("email_cliente")
                .HasMaxLength(100);

            b.HasIndex(x => x.Cpf).IsUnique();
        }
    }
}