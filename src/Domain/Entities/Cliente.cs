using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMoto.src.Domain.Entities
{
    [Table("CLIENTE")]
    public class Cliente
    {
        [Key]
        [Column("ID_CLIENTE")]
        public int IdCliente { get; set; }

        [Column("NOME_CLIENTE")]
        public string NomeCliente { get; set; }

        [Column("CPF_CLIENTE")]
        public string CpfCliente { get; set; }

        [Column("TELEFONE_CLIENTE")]
        public string TelefoneCliente { get; set; }

        [Column("EMAIL_CLIENTE")]
        public string EmailCliente { get; set; }
    }
}
