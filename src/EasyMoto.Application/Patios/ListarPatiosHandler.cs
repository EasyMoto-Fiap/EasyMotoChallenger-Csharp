using EasyMoto.Application.Patios.Contracts;
using EasyMoto.Application.Shared.Pagination;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Patios
{
    public class ListarPatiosHandler
    {
        private readonly IPatioRepository _repo;

        public ListarPatiosHandler(IPatioRepository repo)
        {
            _repo = repo;
        }

        public async Task<PagedResult<PatioResponse>> ExecuteAsync(PageQuery query, CancellationToken ct)
        {
            var total = await _repo.CountAsync(ct);
            var items = await _repo.ListAsync(query.Page, query.Size, ct);
            var mapped = items.Select(e => new PatioResponse
            {
                Id = e.Id,
                NomePatio = e.NomePatio,
                TamanhoPatio = e.TamanhoPatio,
                Andar = e.Andar,
                FilialId = e.FilialId
            });
            return new PagedResult<PatioResponse>(mapped, query.Page, query.Size, total);
        }
    }
}