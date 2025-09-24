namespace EasyMoto.Infrastructure.Persistence.Configurations;

using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("clientes");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .HasMaxLength(120)
            .IsRequired();

        builder.OwnsOne(x => x.Cpf, p =>
        {
            p.Property(v => v.Value)
                .HasColumnName("cpf")
                .HasMaxLength(14)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Telefone, p =>
        {
            p.Property(v => v.Value)
                .HasColumnName("telefone")
                .HasMaxLength(11)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Email, p =>
        {
            p.Property(v => v.Value)
                .HasColumnName("email")
                .HasMaxLength(160)
                .IsRequired();
        });
    }
}