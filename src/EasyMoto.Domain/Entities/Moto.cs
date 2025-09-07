namespace EasyMoto.Domain.Entities;

public enum MotoStatus
{
    Disponivel = 1,
    Alugada = 2,
    Manutencao = 3
}

public sealed class Moto
{
    public int IdMoto { get; private set; }
    public string Modelo { get; private set; } = null!;
    public string Placa { get; private set; } = null!;
    public int Ano { get; private set; }
    public MotoStatus Status { get; private set; }

    private Moto() { }

    public Moto(string modelo, string placa, int ano)
    {
        SetModelo(modelo);
        SetPlaca(placa);
        SetAno(ano);
        Status = MotoStatus.Disponivel;
    }

    public void SetModelo(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Modelo obrigatório");
        Modelo = value.Trim();
    }

    public void SetPlaca(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Placa obrigatória");
        Placa = value.Trim().ToUpperInvariant();
    }

    public void SetAno(int value)
    {
        if (value < 2000) throw new ArgumentException("Ano inválido");
        Ano = value;
    }

    public void Disponibilizar()
    {
        if (Status == MotoStatus.Alugada) throw new InvalidOperationException("Moto alugada");
        Status = MotoStatus.Disponivel;
    }

    public void EnviarParaManutencao()
    {
        if (Status == MotoStatus.Alugada) throw new InvalidOperationException("Moto alugada");
        Status = MotoStatus.Manutencao;
    }

    public void Alugar()
    {
        if (Status != MotoStatus.Disponivel) throw new InvalidOperationException("Moto indisponível");
        Status = MotoStatus.Alugada;
    }

    public void Devolver()
    {
        if (Status != MotoStatus.Alugada) throw new InvalidOperationException("Moto não está alugada");
        Status = MotoStatus.Disponivel;
    }
}
