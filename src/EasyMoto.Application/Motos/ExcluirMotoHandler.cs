using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Motos;

public sealed class ExcluirMotoHandler
{
    private readonly IMotoRepository _repo;
    public ExcluirMotoHandler(IMotoRepository repo) => _repo = repo;

    public async Task ExecuteAsync(int id, CancellationToken ct = default)
    {
        await _repo.DeleteAsync(id, ct);
    }
}