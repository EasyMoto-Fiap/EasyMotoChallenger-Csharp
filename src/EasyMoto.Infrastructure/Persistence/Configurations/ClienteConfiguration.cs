using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations;

public sealed class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("clientes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nome).HasMaxLength(120).IsRequired();

        builder.OwnsOne(x => x.Cpf, cpf =>
        {
            cpf.Property(p => p.Value).HasColumnName("cpf").HasMaxLength(14).IsRequired();
        });
    }
}