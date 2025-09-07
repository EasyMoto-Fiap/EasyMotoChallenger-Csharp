namespace EasyMoto.Application.Locacoes.Contracts;

public sealed class LocacaoResponse
{
    public int IdLocacao { get; init; }
    public int ClienteId { get; init; }
    public DateTime DataInicio { get; init; }
    public DateTime DataFim { get; init; }
    public string StatusLocacao { get; init; } = "";
}
