using System.ComponentModel.DataAnnotations;

namespace EasyMoto.Application.Locacoes.Contracts;

public sealed class AtualizarLocacaoRequest
{
    [Required]
    public DateTime DataInicio { get; init; }

    [Required]
    public DateTime DataFim { get; init; }

    [Required, MinLength(3), MaxLength(50)]
    public string StatusLocacao { get; init; } = "";
}
