using System.ComponentModel.DataAnnotations;

namespace EasyMoto.Application.Motos.Contracts;

public sealed class CriarMotoRequest
{
    [Required, StringLength(10)]
    public string Placa { get; init; } = string.Empty;

    [Required, StringLength(80)]
    public string Marca { get; init; } = string.Empty;

    [Required, StringLength(120)]
    public string Modelo { get; init; } = string.Empty;

    [Required, StringLength(40)]
    public string Cor { get; init; } = string.Empty;

    [Range(1900, 2100)]
    public int AnoFabricacao { get; init; }

    [Required, StringLength(40)]
    public string Status { get; init; } = "Disponivel";

    public int? LocacaoId { get; init; }
    public int LocalizacaoId { get; init; }
}