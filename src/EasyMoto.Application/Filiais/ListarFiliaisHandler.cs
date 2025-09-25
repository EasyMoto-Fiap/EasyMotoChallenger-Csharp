using EasyMoto.Application.Filiais.Contracts;
using EasyMoto.Application.Shared.Pagination;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Filiais
{
    public sealed class ListarFiliaisHandler
    {
        private readonly IFilialRepository _repo;
        public ListarFiliaisHandler(IFilialRepository repo) => _repo = repo;

        public async Task<PagedResult<FilialResponse>> ExecuteAsync(PageQuery query, CancellationToken ct = default)
        {
            var total = await _repo.CountAsync(ct);
            var items = await _repo.ListAsync(query.Page, query.Size, ct);
            var result = items.Select(e => new FilialResponse
            {
                IdFilial = e.IdFilial,
                NomeFilial = e.NomeFilial,
                Cidade = e.Cidade,
                Estado = e.Estado,
                Pais = e.Pais,
                Endereco = e.Endereco,
                EmpresaId = e.EmpresaId
            }).ToList();
            return new PagedResult<FilialResponse>(result, query.Page, query.Size, total);
        }
    }
}