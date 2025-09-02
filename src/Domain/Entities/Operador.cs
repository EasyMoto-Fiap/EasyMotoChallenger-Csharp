using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMoto.src.Domain.Entities
{
    [Table("OPERADOR")]
    public class Operador
    {
        [Key]
        [Column("ID_OPERADOR")]
        public int IdOperador { get; set; }

        [Column("NOME_OPR")]
        public string NomeOpr { get; set; }

        [Column("CPF_OPR")]
        public string CpfOpr { get; set; }

        [Column("TELEFONE_OPR")]
        public string TelefoneOpr { get; set; }

        [Column("EMAIL_OPR")]
        public string EmailOpr { get; set; }

        [Column("FILIAL_ID")]
        public int FilialId { get; set; }

        [ForeignKey("FilialId")]
        public Filial Filial { get; set; }
    }
}
