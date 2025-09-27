namespace EasyMoto.Domain.Entities;

public sealed class Patio
{
    public int IdPatio { get; private set; }
    public string NomePatio { get; private set; } = string.Empty;
    public string TamanhoPatio { get; private set; } = string.Empty;
    public string Andar { get; private set; } = string.Empty;

    public int FilialId { get; private set; }
    public Filial? Filial { get; private set; }

    public ICollection<Vaga> Vagas { get; } = new List<Vaga>();

    public Patio() { }

    public Patio(string nomePatio, string tamanhoPatio, string andar, int filialId)
    {
        NomePatio = nomePatio;
        TamanhoPatio = tamanhoPatio;
        Andar = andar;
        FilialId = filialId;
    }

    public void Update(string nomePatio, string tamanhoPatio, string andar, int filialId)
    {
        NomePatio = nomePatio;
        TamanhoPatio = tamanhoPatio;
        Andar = andar;
        FilialId = filialId;
    }
}