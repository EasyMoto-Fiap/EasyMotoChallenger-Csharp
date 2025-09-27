using System.ComponentModel.DataAnnotations;

namespace EasyMoto.Application.Patios.Contracts
{
    public sealed class CriarPatioRequest
    {
        [Required, StringLength(160)]
        public string NomePatio { get; init; } = string.Empty;

        [Required, StringLength(80)]
        public string TamanhoPatio { get; init; } = string.Empty;

        [Required, StringLength(40)]
        public string Andar { get; init; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int FilialId { get; init; }
    }
}