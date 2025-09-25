using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations
{
    public sealed class FuncionarioConfiguration : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.ToTable("funcionarios");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.NomeFuncionario)
                .HasColumnName("NomeFuncionario")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.Cpf)
                .HasColumnName("Cpf")
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(x => x.FilialId)
                .HasColumnName("FilialId")
                .IsRequired();

            builder.HasOne(x => x.Filial)
                .WithMany()
                .HasForeignKey(x => x.FilialId)
                .HasPrincipalKey(f => f.IdFilial)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.FilialId).HasDatabaseName("IX_funcionarios_FilialId");
        }
    }
}