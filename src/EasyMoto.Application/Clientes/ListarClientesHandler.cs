using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Clientes;

public sealed class ListarClientesHandler
{
    private readonly IClienteRepository _repo;

    public ListarClientesHandler(IClienteRepository repo) => _repo = repo;

    public async Task<List<ClienteResponse>> Handle(CancellationToken ct)
    {
        var list = await _repo.GetAllAsync(ct);
        return list.Select(c => new ClienteResponse
        {
            IdCliente = c.IdCliente,
            NomeCliente = c.NomeCliente,
            CpfCliente = c.CpfCliente.Value,
            TelefoneCliente = c.TelefoneCliente,
            EmailCliente = c.EmailCliente
        }).ToList();
    }
}
