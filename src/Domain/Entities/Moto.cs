using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMoto.src.Domain.Entities
{
    [Table("MOTO")]
    public class Moto
    {
        [Key]
        [Column("ID_MOTO")]
        public int IdMoto { get; set; }

        [Column("PLACA")]
        public string Placa { get; set; }

        [Column("MODELO")]
        public string Modelo { get; set; }

        [Column("ANO_FABRICACAO")]
        public int AnoFabricacao { get; set; }

        [Column("STATUS_MOTO")]
        public string StatusMoto { get; set; }

        [Column("LOCACAO_ID")]
        public int LocacaoId { get; set; }

        [Column("LOCALIZACAO_ID")]
        public int LocalizacaoId { get; set; }

        [ForeignKey("LocacaoId")]
        public ClienteLocacao ClienteLocacao { get; set; }

        [ForeignKey("LocalizacaoId")]
        public Localizacao Localizacao { get; set; }
    }
}
