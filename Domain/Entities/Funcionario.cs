using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMoto.Domain.Entities
{
    [Table("FUNCIONARIO")]
    public class Funcionario
    {
        [Key]
        [Column("ID_FUNC")]
        public int IdFunc { get; set; }

        [Column("NOME_FUNC")]
        public string NomeFunc { get; set; }

        [Column("CPF_FUNC")]
        public string CpfFunc { get; set; }

        [Column("CARGO")]
        public string Cargo { get; set; }

        [Column("TELEFONE_FUNC")]
        public string TelefoneFunc { get; set; }

        [Column("EMAIL_FUNC")]
        public string EmailFunc { get; set; }

        [Column("FILIAL_ID")]
        public int FilialId { get; set; }

        [ForeignKey("FilialId")]
        public Filial Filial { get; set; }
    }
}
