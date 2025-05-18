using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMoto.Domain.Entities
{
    [Table("CLIENTE_LOCACAO")]
    public class ClienteLocacao
    {
        [Key]
        [Column("ID_LOCACAO")]
        public int IdLocacao { get; set; }

        [Column("DATA_INICIO")]
        public DateTime DataInicio { get; set; }

        [Column("DATA_FIM")]
        public DateTime DataFim { get; set; }

        [Column("STATUS_LOCACAO")]
        public string StatusLocacao { get; set; }

        [Column("CLIENTE_ID")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }
    }
}
