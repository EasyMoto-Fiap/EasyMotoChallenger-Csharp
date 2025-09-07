namespace EasyMoto.Application.Motos.Contracts;

public sealed class MotoResponse
{
    public int IdMoto { get; init; }
    public string Modelo { get; init; } = "";
    public string Placa { get; init; } = "";
    public int Ano { get; init; }
    public string Status { get; init; } = "";
}
