using EasyMoto.Domain.ValueObjects;

namespace EasyMoto.Domain.Entities;

public sealed class Cliente
{
    public int IdCliente { get; private set; }
    public string NomeCliente { get; private set; } = null!;
    public Cpf CpfCliente { get; private set; } = null!;
    public string TelefoneCliente { get; private set; } = null!;
    public string EmailCliente { get; private set; } = null!;

    private Cliente() { }

    public Cliente(string nomeCliente, string cpfCliente, string telefoneCliente, string emailCliente)
    {
        SetNome(nomeCliente);
        SetCpf(cpfCliente);
        SetTelefone(telefoneCliente);
        SetEmail(emailCliente);
    }

    public void SetNome(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Nome obrigatório");
        NomeCliente = value.Trim();
    }

    public void SetCpf(string value)
    {
        CpfCliente = Cpf.From(value);
    }

    public void SetTelefone(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Telefone obrigatório");
        TelefoneCliente = value.Trim();
    }

    public void SetEmail(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Email obrigatório");
        EmailCliente = value.Trim();
    }
}
