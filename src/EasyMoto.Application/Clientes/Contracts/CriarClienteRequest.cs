namespace EasyMoto.Application.Clientes.Contracts
{
    public sealed class CriarClienteRequest
    {
        public string Nome { get; init; } = null!;
        public string Cpf { get; init; } = null!;
        public string Telefone { get; init; } = null!;
        public string Email { get; init; } = null!;
    }
}