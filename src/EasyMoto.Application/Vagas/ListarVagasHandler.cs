using EasyMoto.Application.Shared.Pagination;
using EasyMoto.Application.Vagas.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Vagas;

public sealed class ListarVagasHandler
{
    private readonly IVagaRepository _repo;

    public ListarVagasHandler(IVagaRepository repo) => _repo = repo;

    public async Task<PagedResult<VagaResponse>> ExecuteAsync(PageQuery query, CancellationToken ct)
    {
        var total = await _repo.CountAsync(ct);
        var items = await _repo.ListAsync(query.Page, query.Size, ct);

        var mapped = items.Select(e => new VagaResponse
        {
            Id = e.Id,
            PatioId = e.PatioId,
            NumeroVaga = e.NumeroVaga,
            Ocupada = e.Ocupada,
            MotoId = e.MotoId
        }).ToList();

        return new PagedResult<VagaResponse>(mapped, query.Page, query.Size, total);
    }
}