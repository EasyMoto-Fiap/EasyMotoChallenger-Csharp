namespace EasyMoto.Domain.Entities;

using EasyMoto.Domain.ValueObjects;

public sealed class Cliente
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = null!;
    public Cpf Cpf { get; private set; } = null!;
    public Telefone Telefone { get; private set; } = null!;
    public Email Email { get; private set; } = null!;

    private Cliente() { }

    public Cliente(string nome, Cpf cpf, Telefone telefone, Email email)
    {
        Id = Guid.NewGuid();
        Update(nome, cpf, telefone, email);
    }

    public void Update(string nome, Cpf cpf, Telefone telefone, Email email)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome obrigatório");
        Nome = nome.Trim();
        Cpf = cpf ?? throw new ArgumentNullException(nameof(cpf));
        Telefone = telefone ?? throw new ArgumentNullException(nameof(telefone));
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }
}