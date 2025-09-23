using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Motos;

public sealed class ExcluirMotoHandler
{
    private readonly IMotoRepository _repo;
    public ExcluirMotoHandler(IMotoRepository repo) => _repo = repo;

    public async Task<bool> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var existente = await _repo.GetByIdAsync(id, ct);
        if (existente is null) return false;
        await _repo.DeleteAsync(existente, ct);
        return true;
    }
}