namespace EasyMoto.Application.Vagas.Contracts;

public sealed class CriarVagaRequest
{
    public int PatioId { get; init; }
    public int NumeroVaga { get; init; }
    public bool Ocupada { get; init; }
    public int? MotoId { get; init; }
}