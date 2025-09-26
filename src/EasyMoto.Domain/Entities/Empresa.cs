namespace EasyMoto.Domain.Entities;

public sealed class Empresa
{
    public int IdEmpresa { get; private set; }
    public string NomeEmpresa { get; private set; } = null!;
    public string Cnpj { get; private set; } = null!;

    private readonly List<Filial> _filiais = new();
    public IReadOnlyCollection<Filial> Filiais => _filiais.AsReadOnly();

    private Empresa() { }

    public Empresa(string nomeEmpresa, string cnpj)
    {
        Update(nomeEmpresa, cnpj);
    }

    public void Update(string nomeEmpresa, string cnpj)
    {
        if (string.IsNullOrWhiteSpace(nomeEmpresa)) throw new ArgumentException(nameof(nomeEmpresa));
        if (string.IsNullOrWhiteSpace(cnpj)) throw new ArgumentException(nameof(cnpj));
        NomeEmpresa = nomeEmpresa.Trim();
        Cnpj = cnpj.Trim();
    }

    public void AdicionarFilial(Filial filial)
    {
        if (filial == null) throw new ArgumentNullException(nameof(filial));
        var exists = filial.IdFilial > 0
            ? _filiais.Any(f => f.IdFilial == filial.IdFilial)
            : _filiais.Any(f =>
                string.Equals(f.NomeFilial, filial.NomeFilial, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(f.Cidade, filial.Cidade, StringComparison.OrdinalIgnoreCase));
        if (exists) throw new InvalidOperationException("Filial duplicada.");
        _filiais.Add(filial);
    }

    public void RemoverFilial(int filialId)
    {
        var f = _filiais.FirstOrDefault(x => x.IdFilial == filialId);
        if (f == null) throw new KeyNotFoundException("Filial não encontrada.");
        _filiais.Remove(f);
    }
}