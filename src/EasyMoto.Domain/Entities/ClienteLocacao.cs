using System;

namespace EasyMoto.Domain.Entities;

public sealed class ClienteLocacao
{
    public int Id { get; private set; }
    public int ClienteId { get; private set; }
    public int MotoId { get; private set; }
    public DateTime DataInicio { get; private set; }
    public DateTime? DataFim { get; private set; }
    public string StatusLocacao { get; private set; } = string.Empty;

    public Cliente? Cliente { get; private set; }
    public Moto? Moto { get; private set; }

    public ClienteLocacao(int clienteId, int motoId, DateTime dataInicio, DateTime? dataFim, string statusLocacao)
    {
        ClienteId = clienteId;
        MotoId = motoId;
        DataInicio = dataInicio;
        DataFim = dataFim;
        StatusLocacao = statusLocacao;
    }

    public void Atualizar(int clienteId, int motoId, DateTime dataInicio, DateTime? dataFim, string statusLocacao)
    {
        ClienteId = clienteId;
        MotoId = motoId;
        DataInicio = dataInicio;
        DataFim = dataFim;
        StatusLocacao = statusLocacao;
    }
}