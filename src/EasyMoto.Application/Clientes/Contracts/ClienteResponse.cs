namespace EasyMoto.Application.Clientes.Contracts;

public sealed class ClienteResponse
{
    public Guid Id { get; init; }
    public string Nome { get; init; } = string.Empty;
    public string Cpf { get; init; } = string.Empty;
}