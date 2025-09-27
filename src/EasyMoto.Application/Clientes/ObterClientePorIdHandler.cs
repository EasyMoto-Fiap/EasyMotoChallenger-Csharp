using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Clientes
{
    public sealed class ObterClientePorIdHandler
    {
        private readonly IClienteRepository _repo;
        public ObterClientePorIdHandler(IClienteRepository repo) => _repo = repo;

        public async Task<ClienteResponse?> ExecuteAsync(int id, CancellationToken ct = default)
        {
            var e = await _repo.GetByIdAsync(id, ct);
            if (e is null) return null;

            return new ClienteResponse
            {
                Id = e.Id,
                Nome = e.NomeCliente,
                Cpf = e.CpfCliente,
                Telefone = e.TelefoneCliente,
                Email = e.EmailCliente
            };
        }
    }
}