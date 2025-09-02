using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMoto.src.Domain.Entities
{
    [Table("PATIO")]
    public class Patio
    {
        [Key]
        [Column("ID_PATIO")]
        public int IdPatio { get; set; }

        [Column("NOME_PATIO")]
        public string? NomePatio { get; set; }

        [Column("TAMANHO_PATIO")]
        public string TamanhoPatio { get; set; }

        [Column("ANDAR")]
        public string? Andar { get; set; }

        [Column("FILIAL_ID")]
        public int FilialId { get; set; }

        [ForeignKey("FilialId")]
        public Filial Filial { get; set; }
    }
}
