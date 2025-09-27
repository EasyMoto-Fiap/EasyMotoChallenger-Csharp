namespace EasyMoto.Application.Motos.Contracts;

public sealed class MotoResponse
{
    public int Id { get; init; }
    public string Placa { get; init; } = string.Empty;
    public string Marca { get; init; } = string.Empty;
    public string Modelo { get; init; } = string.Empty;
    public string Cor { get; init; } = string.Empty;
    public int AnoFabricacao { get; init; }
    public string Status { get; init; } = string.Empty;
    public int? LocacaoId { get; init; }
    public int LocalizacaoId { get; init; }
}