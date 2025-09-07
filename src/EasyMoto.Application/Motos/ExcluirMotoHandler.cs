using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Motos;

public sealed class ExcluirMotoHandler
{
    private readonly IMotoRepository _repo;

    public ExcluirMotoHandler(IMotoRepository repo) => _repo = repo;

    public async Task<bool> Handle(int id, CancellationToken ct)
    {
        var m = await _repo.GetByIdAsync(id, ct);
        if (m is null) return false;
        await _repo.DeleteAsync(id, ct);
        await _repo.SaveChangesAsync(ct);
        return true;
    }
}
