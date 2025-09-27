using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations;

public sealed class FuncionarioConfiguration : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> b)
    {
        b.ToTable("funcionarios");
        b.HasKey(x => x.Id).HasName("pk_funcionarios");
        b.Property(x => x.Id).ValueGeneratedOnAdd();
        b.Property(x => x.NomeFuncionario).HasMaxLength(120).IsRequired();
        b.Property(x => x.Cpf).HasMaxLength(14).IsRequired();
        b.Property(x => x.FilialId).IsRequired();
        b.HasIndex(x => x.FilialId).HasDatabaseName("IX_funcionarios_FilialId");
        b.HasOne(x => x.Filial)
            .WithMany()
            .HasForeignKey(x => x.FilialId)
            .HasConstraintName("fk_funcionarios_filiais_filial_id")
            .OnDelete(DeleteBehavior.Restrict);
    }
}