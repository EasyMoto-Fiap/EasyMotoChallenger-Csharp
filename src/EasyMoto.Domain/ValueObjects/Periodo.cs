using EasyMoto.Domain.Abstractions;

namespace EasyMoto.Domain.ValueObjects;

public sealed class Periodo : ValueObject
{
    public DateTime Inicio { get; }
    public DateTime Fim { get; }

    private Periodo(DateTime inicio, DateTime fim)
    {
        Inicio = inicio;
        Fim = fim;
    }

    public static Periodo Create(DateTime inicio, DateTime fim)
    {
        if (fim < inicio) throw new ArgumentException("Período inválido");
        return new Periodo(inicio, fim);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Inicio;
        yield return Fim;
    }
}