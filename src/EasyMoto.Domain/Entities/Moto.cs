namespace EasyMoto.Domain.Entities;

public enum MotoStatus
{
    Disponivel = 0,
    Alugada = 1,
    Manutencao = 2
}

public sealed class Moto
{
    public Guid Id { get; private set; }
    public string Placa { get; private set; } = string.Empty;
    public MotoStatus Status { get; private set; }

    private Moto() { }

    public Moto(Guid id, string placa)
    {
        Id = id == default ? Guid.NewGuid() : id;
        SetPlaca(placa);
        Status = MotoStatus.Disponivel;
    }

    public void SetPlaca(string placa)
    {
        if (string.IsNullOrWhiteSpace(placa)) throw new ArgumentException("Placa inválida");
        Placa = placa.Trim().ToUpperInvariant();
    }

    public void MarcarAlugada()
    {
        if (Status != MotoStatus.Disponivel) throw new InvalidOperationException("Moto indisponível");
        Status = MotoStatus.Alugada;
    }

    public void MarcarDevolvida()
    {
        if (Status != MotoStatus.Alugada) throw new InvalidOperationException("Moto não está alugada");
        Status = MotoStatus.Disponivel;
    }

    public void MarcarManutencao()
    {
        if (Status == MotoStatus.Alugada) throw new InvalidOperationException("Moto alugada");
        Status = MotoStatus.Manutencao;
    }

    public void SairDeManutencao()
    {
        if (Status != MotoStatus.Manutencao) throw new InvalidOperationException("Moto não está em manutenção");
        Status = MotoStatus.Disponivel;
    }
}