using EasyMoto.Application.Empresas.Contracts;
using EasyMoto.Application.Shared.Pagination;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Empresas;

public sealed class ListarEmpresasHandler
{
    private readonly IEmpresaRepository _repo;
    public ListarEmpresasHandler(IEmpresaRepository repo) => _repo = repo;

    public async Task<PagedResult<EmpresaResponse>> ExecuteAsync(PageQuery query, CancellationToken ct = default)
    {
        var total = await _repo.CountAsync(ct);
        var items = await _repo.ListAsync(query.Page, query.Size, ct);
        var result = items.Select(e => new EmpresaResponse(e.IdEmpresa, e.NomeEmpresa, e.Cnpj)).ToList();
        return new PagedResult<EmpresaResponse>(result, query.Page, query.Size, total);
    }
}