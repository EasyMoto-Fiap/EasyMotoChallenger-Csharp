using EasyMoto.Application.Shared.Pagination;
using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Clientes;

public sealed class ListarClientesHandler
{
    private readonly IClienteRepository _repo;

    public ListarClientesHandler(IClienteRepository repo) => _repo = repo;

    public async Task<PagedResult<ClienteResponse>> ExecuteAsync(PageQuery query, CancellationToken ct = default)
    {
        var total = await _repo.CountAsync(ct);
        var items = await _repo.ListAsync(query.Page, query.Size, ct);

        var mapped = items.Select(e => new ClienteResponse
        {
            Id = e.Id,
            Nome = e.NomeCliente,
            Cpf = e.CpfCliente,
            Telefone = e.TelefoneCliente,
            Email = e.EmailCliente
        }).ToList();

        return new PagedResult<ClienteResponse>(mapped, query.Page, query.Size, total);
    }
}