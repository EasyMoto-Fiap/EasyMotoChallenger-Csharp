namespace EasyMoto.Application.Locacoes.Contracts;

public sealed class LocacaoResponse
{
    public Guid Id { get; init; }
    public Guid ClienteId { get; init; }
    public Guid MotoId { get; init; }
    public DateTime Inicio { get; init; }
    public DateTime Fim { get; init; }
}