using EasyMoto.Domain.Abstractions;

namespace EasyMoto.Domain.ValueObjects;

public sealed class Periodo : ValueObject
{
    public DateTime Inicio { get; }
    public DateTime Fim { get; }

    private Periodo(DateTime inicio, DateTime fim)
    {
        if (fim < inicio) throw new ArgumentException("Fim deve ser >= início");
        Inicio = inicio;
        Fim = fim;
    }

    public static Periodo De(DateTime inicio, DateTime fim) => new(inicio, fim);

    public int Dias
    {
        get
        {
            var dias = (int)Math.Ceiling((Fim.Date - Inicio.Date).TotalDays);
            return Math.Max(dias, 1);
        }
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Inicio;
        yield return Fim;
    }
}
