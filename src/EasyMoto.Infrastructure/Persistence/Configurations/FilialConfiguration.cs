using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations
{
    public class FilialConfiguration : IEntityTypeConfiguration<Filial>
    {
        public void Configure(EntityTypeBuilder<Filial> builder)
        {
            builder.ToTable("filiais");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(120).IsRequired();
            builder.Property(x => x.Cep).HasColumnName("cep").HasMaxLength(10).IsRequired();
            builder.Property(x => x.Cidade).HasColumnName("cidade").HasMaxLength(80).IsRequired();
            builder.Property(x => x.Uf).HasColumnName("uf").HasMaxLength(2).IsRequired();
        }
    }
}