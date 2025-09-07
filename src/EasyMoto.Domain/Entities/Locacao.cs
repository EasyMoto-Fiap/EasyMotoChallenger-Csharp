using EasyMoto.Domain.ValueObjects;

namespace EasyMoto.Domain.Entities;

public enum LocacaoStatus
{
    Aberta = 1,
    Encerrada = 2,
    Cancelada = 3
}

public sealed class Locacao
{
    public int IdLocacao { get; private set; }
    public int ClienteId { get; private set; }
    public int MotoId { get; private set; }

    public Periodo Periodo { get; private set; } = null!;
    public decimal ValorDiaria { get; private set; }
    public decimal ValorTotal { get; private set; }

    public LocacaoStatus Status { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public DateTime? EncerradoEm { get; private set; }

    private Locacao() { }

    public Locacao(int clienteId, int motoId, DateTime inicio, DateTime fim, decimal valorDiaria)
    {
        SetCliente(clienteId);
        SetMoto(motoId);
        DefinirPeriodo(inicio, fim);
        DefinirValorDiaria(valorDiaria);
        RecalcularTotal();
        Status = LocacaoStatus.Aberta;
        CriadoEm = DateTime.UtcNow;
    }

    public void DefinirPeriodo(DateTime inicio, DateTime fim)
    {
        Periodo = Periodo.De(inicio, fim);
        RecalcularTotal();
    }

    public void DefinirValorDiaria(decimal valor)
    {
        if (valor <= 0) throw new ArgumentException("Valor da diária deve ser > 0");
        ValorDiaria = valor;
        RecalcularTotal();
    }

    public void Encerrar(DateTime dataDevolucao)
    {
        if (Status != LocacaoStatus.Aberta) throw new InvalidOperationException("Locação não está aberta");
        DefinirPeriodo(Periodo.Inicio, dataDevolucao);
        Status = LocacaoStatus.Encerrada;
        EncerradoEm = DateTime.UtcNow;
    }

    public void Cancelar()
    {
        if (Status != LocacaoStatus.Aberta) throw new InvalidOperationException("Somente locações abertas podem ser canceladas");
        Status = LocacaoStatus.Cancelada;
        EncerradoEm = DateTime.UtcNow;
    }

    public void SetCliente(int id)
    {
        if (id <= 0) throw new ArgumentException("Cliente inválido");
        ClienteId = id;
    }

    public void SetMoto(int id)
    {
        if (id <= 0) throw new ArgumentException("Moto inválida");
        MotoId = id;
    }

    private void RecalcularTotal()
    {
        if (Periodo is null) return;
        if (ValorDiaria <= 0) return;
        ValorTotal = ValorDiaria * Periodo.Dias;
    }
}
