using EasyMoto.Application.ClienteLocacoes.Contracts;
using EasyMoto.Application.Shared.Pagination;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.ClienteLocacoes;

public sealed class ListarClienteLocacoesHandler
{
    private readonly IClienteLocacaoRepository _repo;

    public ListarClienteLocacoesHandler(IClienteLocacaoRepository repo) => _repo = repo;

    public async Task<PagedResult<ClienteLocacaoResponse>> ExecuteAsync(PageQuery query, CancellationToken ct = default)
    {
        var total = await _repo.CountAsync(ct);
        var items = await _repo.ListAsync(query.Page, query.Size, ct);

        var mapped = items.Select(e => new ClienteLocacaoResponse
        {
            Id = e.Id,
            ClienteId = e.ClienteId,
            MotoId = e.MotoId,
            DataInicio = e.DataInicio,
            DataFim = e.DataFim,
            StatusLocacao = e.StatusLocacao
        }).ToList();

        return new PagedResult<ClienteLocacaoResponse>(mapped, query.Page, query.Size, total);
    }
}