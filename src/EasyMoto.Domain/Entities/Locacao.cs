using EasyMoto.Domain.ValueObjects;

namespace EasyMoto.Domain.Entities;

public sealed class Locacao
{
    public Guid Id { get; private set; }
    public Guid ClienteId { get; private set; }
    public Guid MotoId { get; private set; }
    public Periodo Periodo { get; private set; } = null!;

    private Locacao() { }

    public Locacao(Guid id, Guid clienteId, Guid motoId, Periodo periodo)
    {
        Id = id == default ? Guid.NewGuid() : id;
        if (clienteId == Guid.Empty) throw new ArgumentException("ClienteId inválido");
        if (motoId == Guid.Empty) throw new ArgumentException("MotoId inválido");
        ClienteId = clienteId;
        MotoId = motoId;
        Periodo = periodo;
    }
}