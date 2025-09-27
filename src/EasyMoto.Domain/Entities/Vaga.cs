namespace EasyMoto.Domain.Entities;

public sealed class Vaga
{
    public int Id { get; private set; }
    public int PatioId { get; private set; }
    public string NumeroVaga { get; private set; } = default!;
    public bool Ocupada { get; private set; }
    public int? MotoId { get; private set; }

    public Vaga(int patioId, string numeroVaga, bool ocupada, int? motoId)
    {
        PatioId = patioId;
        NumeroVaga = numeroVaga;
        Ocupada = ocupada;
        MotoId = motoId;
    }

    public void Update(int patioId, string numeroVaga, bool ocupada, int? motoId)
    {
        PatioId = patioId;
        NumeroVaga = numeroVaga;
        Ocupada = ocupada;
        MotoId = motoId;
    }

    private Vaga() { }
}