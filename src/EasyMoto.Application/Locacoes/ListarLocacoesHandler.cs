using EasyMoto.Application.Locacoes.Contracts;
using EasyMoto.Application.Shared.Pagination;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Locacoes;

public sealed class ListarLocacoesHandler
{
    private readonly ILocacaoRepository _repo;
    public ListarLocacoesHandler(ILocacaoRepository repo) => _repo = repo;

    public async Task<PagedResult<LocacaoResponse>> ExecuteAsync(PageQuery query, CancellationToken ct = default)
    {
        var total = await _repo.CountAsync(ct);
        var items = await _repo.ListAsync(query.Page, query.Size, ct);
        var result = items.Select(l => new LocacaoResponse
        {
            Id = l.Id,
            ClienteId = l.ClienteId,
            MotoId = l.MotoId,
            Inicio = l.Periodo.Inicio,
            Fim = l.Periodo.Fim
        });
        return new PagedResult<LocacaoResponse>(result, query.Page, query.Size, total);
    }
}