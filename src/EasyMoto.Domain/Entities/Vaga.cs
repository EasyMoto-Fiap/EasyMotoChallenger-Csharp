namespace EasyMoto.Domain.Entities;

public sealed class Vaga
{
    public int Id { get; private set; }
    public int PatioId { get; private set; }
    public Patio Patio { get; private set; } = null!;
    public int NumeroVaga { get; private set; }
    public bool Ocupada { get; private set; }
    public int? MotoId { get; private set; }
    public Moto? Moto { get; private set; }

    public Vaga() { }

    public Vaga(int patioId, int numeroVaga, bool ocupada, int? motoId)
    {
        PatioId = patioId;
        NumeroVaga = numeroVaga;
        Ocupada = ocupada;
        MotoId = motoId;
    }

    public void Update(int patioId, int numeroVaga, bool ocupada, int? motoId)
    {
        PatioId = patioId;
        NumeroVaga = numeroVaga;
        Ocupada = ocupada;
        MotoId = motoId;
    }
}