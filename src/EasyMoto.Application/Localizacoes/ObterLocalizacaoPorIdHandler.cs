using EasyMoto.Application.Localizacoes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Localizacoes;

public sealed class ObterLocalizacaoPorIdHandler
{
    private readonly ILocalizacaoRepository _repo;

    public ObterLocalizacaoPorIdHandler(ILocalizacaoRepository repo) => _repo = repo;

    public async Task<LocalizacaoResponse?> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var e = await _repo.GetByIdAsync(id, ct);
        if (e is null) return null;
        return new LocalizacaoResponse
        {
            Id = e.Id,
            StatusLoc = e.StatusLoc,
            DataHora = e.DataHora,
            ZonaVirtual = e.ZonaVirtual,
            Latitude = e.Latitude,
            Longitude = e.Longitude
        };
    }
}