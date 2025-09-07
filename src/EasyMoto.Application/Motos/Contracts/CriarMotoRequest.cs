namespace EasyMoto.Application.Motos.Contracts;

public sealed class CriarMotoRequest
{
    public string Modelo { get; init; } = "";
    public string Placa { get; init; } = "";
    public int Ano { get; init; }
}
