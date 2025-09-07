using EasyMoto.Domain.ValueObjects;

namespace EasyMoto.Domain.Entities;

public sealed class Locacao
{
    public int IdLocacao { get; private set; }
    public int ClienteId { get; private set; }
    public Periodo Periodo { get; private set; } = null!;
    public string StatusLocacao { get; private set; } = null!;

    private Locacao() { }

    public Locacao(int clienteId, DateTime dataInicio, DateTime dataFim, string status)
    {
        SetCliente(clienteId);
        DefinirPeriodo(dataInicio, dataFim);
        DefinirStatus(status);
    }

    public void SetCliente(int id)
    {
        if (id <= 0) throw new ArgumentException("Cliente inválido");
        ClienteId = id;
    }

    public void DefinirPeriodo(DateTime inicio, DateTime fim)
    {
        Periodo = Periodo.De(inicio, fim);
    }

    public void DefinirStatus(string status)
    {
        if (string.IsNullOrWhiteSpace(status)) throw new ArgumentException("Status obrigatório");
        StatusLocacao = status.Trim();
    }
}
