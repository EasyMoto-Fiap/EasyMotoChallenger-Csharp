namespace EasyMoto.Application.Clientes.Contracts;

public sealed class AtualizarClienteRequest
{
    public string NomeCliente { get; init; } = "";
    public string CpfCliente { get; init; } = "";
    public string TelefoneCliente { get; init; } = "";
    public string EmailCliente { get; init; } = "";
}
