using System.ComponentModel.DataAnnotations;

namespace EasyMoto.Application.Locacoes.Contracts;

public sealed class AtualizarLocacaoRequest
{
    [Required]
    public DateTime Inicio { get; init; }

    [Required]
    public DateTime Fim { get; init; }
}