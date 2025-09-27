using EasyMoto.Application.Shared.Pagination;
using EasyMoto.Application.Localizacoes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Localizacoes;

public sealed class ListarLocalizacoesHandler
{
    private readonly ILocalizacaoRepository _repo;

    public ListarLocalizacoesHandler(ILocalizacaoRepository repo) => _repo = repo;

    public async Task<PagedResult<LocalizacaoResponse>> ExecuteAsync(PageQuery query, CancellationToken ct = default)
    {
        var total = await _repo.CountAsync(ct);
        var items = await _repo.ListAsync(query.Page, query.Size, ct);

        var mapped = items.Select(e => new LocalizacaoResponse
        {
            Id = e.Id,
            StatusLoc = e.StatusLoc,
            DataHora = e.DataHora,
            ZonaVirtual = e.ZonaVirtual,
            Latitude = e.Latitude,
            Longitude = e.Longitude
        }).ToList();

        return new PagedResult<LocalizacaoResponse>(mapped, query.Page, query.Size, total);
    }
}