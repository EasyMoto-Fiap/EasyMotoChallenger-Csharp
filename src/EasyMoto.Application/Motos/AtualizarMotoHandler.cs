using EasyMoto.Application.Motos.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Motos;

public sealed class AtualizarMotoHandler
{
    private readonly IMotoRepository _repo;
    public AtualizarMotoHandler(IMotoRepository repo) => _repo = repo;

    public async Task<MotoResponse?> ExecuteAsync(Guid id, AtualizarMotoRequest request, CancellationToken ct = default)
    {
        var existente = await _repo.GetByIdAsync(id, ct);
        if (existente is null) return null;
        existente.SetPlaca(request.Placa);
        await _repo.UpdateAsync(existente, ct);
        return new MotoResponse { Id = existente.Id, Placa = existente.Placa, Status = existente.Status };
    }
}