using System.ComponentModel.DataAnnotations;

namespace EasyMoto.Application.Locacoes.Contracts;

public sealed class CriarLocacaoRequest
{
    [Range(1, int.MaxValue)]
    public int ClienteId { get; init; }

    [Required]
    public DateTime DataInicio { get; init; }

    [Required]
    public DateTime DataFim { get; init; }

    [Required, MinLength(3), MaxLength(50)]
    public string StatusLocacao { get; init; } = "";
}
