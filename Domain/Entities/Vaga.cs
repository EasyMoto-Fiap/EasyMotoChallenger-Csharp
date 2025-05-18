using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMoto.Domain.Entities
{
    [Table("VAGA")]
    public class Vaga
    {
        [Key]
        [Column("ID_VAGA")]
        public int IdVaga { get; set; }

        [Column("STATUS_VAGA")]
        public string StatusVaga { get; set; }

        [Column("PATIO_ID")]
        public int PatioId { get; set; }

        [Column("MOTO_ID")]
        public int MotoId { get; set; }

        [Column("FILEIRA")]
        public string Fileira { get; set; }

        [Column("COLUNA")]
        public string Coluna { get; set; }

        [ForeignKey("PatioId")]
        public Patio Patio { get; set; }

        [ForeignKey("MotoId")]
        public Moto Moto { get; set; }
    }
}
