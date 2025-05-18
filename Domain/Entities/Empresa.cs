using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMoto.Domain.Entities
{
    [Table("EMPRESA")]
    public class Empresa
    {
        [Key]
        [Column("ID_EMPRESA")]
        public int IdEmpresa { get; set; }

        [Column("NOME_EMPRESA")]
        public string NomeEmpresa { get; set; }

        [Column("CNPJ")]
        public string Cnpj { get; set; }
    }
}
