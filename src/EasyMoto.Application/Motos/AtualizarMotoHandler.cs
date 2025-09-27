using EasyMoto.Application.Motos.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Motos;

public sealed class AtualizarMotoHandler
{
    private readonly IMotoRepository _repo;
    public AtualizarMotoHandler(IMotoRepository repo) => _repo = repo;

    public async Task<bool> ExecuteAsync(AtualizarMotoRequest req, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(req.Id, ct);
        if (entity is null) return false;

        entity.Update(
            req.Placa, req.Marca, req.Modelo, req.Cor,
            req.AnoFabricacao, req.Status, req.LocacaoId, req.LocalizacaoId);

        await _repo.UpdateAsync(entity, ct);
        return true;
    }
}