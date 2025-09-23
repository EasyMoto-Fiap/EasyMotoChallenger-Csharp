using EasyMoto.Domain.ValueObjects;

namespace EasyMoto.Domain.Entities;

public sealed class Cliente
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public Cpf Cpf { get; private set; } = null!;

    private Cliente() { }

    public Cliente(Guid id, string nome, Cpf cpf)
    {
        Id = id == default ? Guid.NewGuid() : id;
        UpdateNome(nome);
        Cpf = cpf;
    }

    public void UpdateNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome inválido");
        Nome = nome.Trim();
    }
}