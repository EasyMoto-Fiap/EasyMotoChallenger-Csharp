using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Clientes;

public sealed class AtualizarClienteHandler
{
    private readonly IClienteRepository _repo;
    public AtualizarClienteHandler(IClienteRepository repo) => _repo = repo;

    public async Task<ClienteResponse?> ExecuteAsync(Guid id, AtualizarClienteRequest request, CancellationToken ct = default)
    {
        var existente = await _repo.GetByIdAsync(id, ct);
        if (existente is null) return null;
        existente.UpdateNome(request.Nome);
        await _repo.UpdateAsync(existente, ct);
        return new ClienteResponse { Id = existente.Id, Nome = existente.Nome, Cpf = existente.Cpf.Value };
    }
}