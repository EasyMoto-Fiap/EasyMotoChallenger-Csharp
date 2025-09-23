using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Clientes;

public sealed class ExcluirClienteHandler
{
    private readonly IClienteRepository _repo;
    public ExcluirClienteHandler(IClienteRepository repo) => _repo = repo;

    public async Task<bool> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var existente = await _repo.GetByIdAsync(id, ct);
        if (existente is null) return false;
        await _repo.DeleteAsync(existente, ct);
        return true;
    }
}