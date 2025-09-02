using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMoto.src.Domain.Entities
{
    [Table("LOCALIZACAO")]
    public class Localizacao
    {
        [Key]
        [Column("ID_LOCALIZACAO")]
        public int IdLocalizacao { get; set; }

        [Column("STATUS_LOC")]
        public string StatusLoc { get; set; }

        [Column("DATA_HORA")]
        public DateTime? DataHora { get; set; }

        [Column("ZONA_VIRTUAL")]
        public string? ZonaVirtual { get; set; }

        [Column("LATITUDE")]
        public double? Latitude { get; set; }

        [Column("LONGITUDE")]
        public double? Longitude { get; set; }
    }
}
