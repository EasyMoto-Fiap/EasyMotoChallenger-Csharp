using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations
{
    public sealed class FilialConfiguration : IEntityTypeConfiguration<Filial>
    {
        public void Configure(EntityTypeBuilder<Filial> builder)
        {
            builder.ToTable("filiais");
            builder.HasKey(x => x.IdFilial);
            builder.Property(x => x.IdFilial).HasColumnName("IdFilial");
            builder.Property(x => x.NomeFilial).HasColumnName("NomeFilial").HasMaxLength(160).IsRequired();
            builder.Property(x => x.Cidade).HasColumnName("Cidade").HasMaxLength(120);
            builder.Property(x => x.Estado).HasColumnName("Estado").HasMaxLength(80);
            builder.Property(x => x.Pais).HasColumnName("Pais").HasMaxLength(80);
            builder.Property(x => x.Endereco).HasColumnName("Endereco").HasMaxLength(200);
            builder.Property(x => x.EmpresaId).HasColumnName("EmpresaId").IsRequired();
            builder.HasOne(x => x.Empresa)
                .WithMany()
                .HasForeignKey(x => x.EmpresaId)
                .HasPrincipalKey(e => e.IdEmpresa)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}