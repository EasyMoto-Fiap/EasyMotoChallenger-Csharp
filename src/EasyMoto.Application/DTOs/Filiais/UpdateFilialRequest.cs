using System.ComponentModel.DataAnnotations;

namespace EasyMoto.Application.DTOs.Filiais
{
    public class UpdateFilialRequest
    {
        [Required]
        [MaxLength(120)]
        public required string Nome { get; set; }

        [Required]
        [MaxLength(10)]
        public required string Cep { get; set; }

        [Required]
        [MaxLength(80)]
        public required string Cidade { get; set; }

        [Required]
        [MaxLength(2)]
        public required string Uf { get; set; }
    }
}