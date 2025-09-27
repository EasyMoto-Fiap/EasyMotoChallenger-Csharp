using EasyMoto.Application.Localizacoes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Localizacoes;

public sealed class AtualizarLocalizacaoHandler
{
    private readonly ILocalizacaoRepository _repo;

    public AtualizarLocalizacaoHandler(ILocalizacaoRepository repo) => _repo = repo;

    public async Task<bool> ExecuteAsync(AtualizarLocalizacaoRequest req, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(req.Id, ct);
        if (entity is null) return false;
        entity.Atualizar(req.StatusLoc, req.DataHora, req.ZonaVirtual, req.Latitude, req.Longitude);
        await _repo.UpdateAsync(entity, ct);
        return true;
    }
}