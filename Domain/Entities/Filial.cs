using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMoto.Domain.Entities
{
    [Table("FILIAL")]
    public class Filial
    {
        [Key]
        [Column("ID_FILIAL")]
        public int IdFilial { get; set; }

        [Column("NOME_FILIAL")]
        public string NomeFilial { get; set; }

        [Column("CIDADE")]
        public string Cidade { get; set; }

        [Column("ESTADO")]
        public string Estado { get; set; }

        [Column("PAIS")]
        public string Pais { get; set; }

        [Column("ENDERECO")]
        public string Endereco { get; set; }

        [Column("EMPRESA_ID")]
        public int EmpresaId { get; set; }

        [ForeignKey("EmpresaId")]
        public Empresa Empresa { get; set; }
    }
}
