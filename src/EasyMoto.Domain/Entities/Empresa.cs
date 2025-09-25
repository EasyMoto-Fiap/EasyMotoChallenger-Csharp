namespace EasyMoto.Domain.Entities;

public sealed class Empresa
{
    public int IdEmpresa { get; private set; }
    public string NomeEmpresa { get; private set; } = null!;
    public string Cnpj { get; private set; } = null!;

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
}