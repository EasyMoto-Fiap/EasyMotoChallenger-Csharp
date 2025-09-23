using System.ComponentModel.DataAnnotations;

namespace EasyMoto.Application.Locacoes.Contracts;

public sealed class CriarLocacaoRequest
{
    [Required]
    public Guid ClienteId { get; init; }

    [Required]
    public Guid MotoId { get; init; }

    [Required]
    public DateTime Inicio { get; init; }

    [Required]
    public DateTime Fim { get; init; }
}