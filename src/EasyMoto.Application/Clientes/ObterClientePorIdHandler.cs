using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Clientes;

public sealed class ObterClientePorIdHandler
{
    private readonly IClienteRepository _repo;

    public ObterClientePorIdHandler(IClienteRepository repo) => _repo = repo;

    public async Task<ClienteResponse?> Handle(int id, CancellationToken ct)
    {
        var c = await _repo.GetByIdAsync(id, ct);
        if (c is null) return null;
        return new ClienteResponse
        {
            IdCliente = c.IdCliente,
            NomeCliente = c.NomeCliente,
            CpfCliente = c.CpfCliente,
            TelefoneCliente = c.TelefoneCliente ?? "",
            EmailCliente = c.EmailCliente ?? ""
        };
    }
}
