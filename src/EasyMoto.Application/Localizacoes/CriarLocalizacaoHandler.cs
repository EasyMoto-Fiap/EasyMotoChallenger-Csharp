using EasyMoto.Application.Localizacoes.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Localizacoes;

public sealed class CriarLocalizacaoHandler
{
    private readonly ILocalizacaoRepository _repo;

    public CriarLocalizacaoHandler(ILocalizacaoRepository repo) => _repo = repo;

    public async Task<LocalizacaoResponse> ExecuteAsync(CriarLocalizacaoRequest req, CancellationToken ct = default)
    {
        var entity = new Localizacao(req.StatusLoc, req.DataHora, req.ZonaVirtual, req.Latitude, req.Longitude);
        await _repo.AddAsync(entity, ct);
        return new LocalizacaoResponse
        {
            Id = entity.Id,
            StatusLoc = entity.StatusLoc,
            DataHora = entity.DataHora,
            ZonaVirtual = entity.ZonaVirtual,
            Latitude = entity.Latitude,
            Longitude = entity.Longitude
        };
    }
}