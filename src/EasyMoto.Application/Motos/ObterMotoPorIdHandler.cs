using EasyMoto.Application.Motos.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Motos;

public sealed class ObterMotoPorIdHandler
{
    private readonly IMotoRepository _repo;
    public ObterMotoPorIdHandler(IMotoRepository repo) => _repo = repo;

    public async Task<MotoResponse?> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var m = await _repo.GetByIdAsync(id, ct);
        return m is null ? null : new MotoResponse { Id = m.Id, Placa = m.Placa, Status = m.Status };
    }
}