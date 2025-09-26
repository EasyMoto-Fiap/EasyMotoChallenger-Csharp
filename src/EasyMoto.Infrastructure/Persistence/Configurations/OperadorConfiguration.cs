using EasyMoto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyMoto.Infrastructure.Persistence.Configurations
{
    public class OperadorConfiguration : IEntityTypeConfiguration<Operador>
    {
        public void Configure(EntityTypeBuilder<Operador> b)
        {
            b.ToTable("operador");

            b.HasKey(x => x.Id);

            b.Property(x => x.Id)
                .HasColumnName("id_operador")
                .ValueGeneratedOnAdd();

            b.Property(x => x.NomeOperador)
                .HasColumnName("nome_opr")
                .HasMaxLength(100)
                .IsRequired();

            b.Property(x => x.Cpf)
                .HasColumnName("cpf_opr")
                .HasMaxLength(11)
                .IsRequired();

            b.Property(x => x.Telefone)
                .HasColumnName("telefone_opr")
                .HasMaxLength(14)
                .IsRequired();

            b.Property(x => x.Email)
                .HasColumnName("email_opr")
                .HasMaxLength(100)
                .IsRequired();

            b.Property(x => x.FilialId)
                .HasColumnName("filial_id")
                .IsRequired();

            b.HasIndex(x => x.Cpf).IsUnique();

            b.HasOne<Filial>()
                .WithMany()
                .HasForeignKey(x => x.FilialId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}