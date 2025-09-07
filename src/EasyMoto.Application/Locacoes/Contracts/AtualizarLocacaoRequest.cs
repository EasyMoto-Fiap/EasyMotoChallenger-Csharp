namespace EasyMoto.Application.Locacoes.Contracts;

public sealed class AtualizarLocacaoRequest
{
    public DateTime Inicio { get; init; }
    public DateTime Fim { get; init; }
    public decimal ValorDiaria { get; init; }
}
