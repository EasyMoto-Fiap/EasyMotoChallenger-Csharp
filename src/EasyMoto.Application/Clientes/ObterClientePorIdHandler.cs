using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Clientes;

public sealed class ObterClientePorIdHandler
{
    private readonly IClienteRepository _repo;
    public ObterClientePorIdHandler(IClienteRepository repo) => _repo = repo;

    public async Task<ClienteResponse?> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var c = await _repo.GetByIdAsync(id, ct);
        return c is null ? null : new ClienteResponse { Id = c.Id, Nome = c.Nome, Cpf = c.Cpf.Value };
    }
}