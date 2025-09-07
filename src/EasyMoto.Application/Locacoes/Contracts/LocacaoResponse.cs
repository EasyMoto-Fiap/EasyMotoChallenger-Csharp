using System;

namespace EasyMoto.Application.Locacoes.Contracts;

public sealed class LocacaoResponse
{
    public int IdLocacao { get; init; }
    public int ClienteId { get; init; }
    public int MotoId { get; init; }
    public DateTime Inicio { get; init; }
    public DateTime Fim { get; init; }
    public decimal ValorDiaria { get; init; }
    public decimal ValorTotal { get; init; }
    public string Status { get; init; } = "";
}
