namespace EasyMoto.Domain.Entities;

public sealed class Patio
{
    public int Id { get; private set; }
    public string NomePatio { get; private set; } = string.Empty;
    public string TamanhoPatio { get; private set; } = string.Empty;
    public string? Andar { get; private set; }
    public int FilialId { get; private set; }
    public ICollection<Vaga> Vagas { get; } = new List<Vaga>();

    private Patio() { }

    public Patio(string nomePatio, string tamanhoPatio, string? andar, int filialId)
    {
        NomePatio = nomePatio;
        TamanhoPatio = tamanhoPatio;
        Andar = andar;
        FilialId = filialId;
    }

    public void Atualizar(string nomePatio, string tamanhoPatio, string? andar, int filialId)
    {
        NomePatio = nomePatio;
        TamanhoPatio = tamanhoPatio;
        Andar = andar;
        FilialId = filialId;
    }
}