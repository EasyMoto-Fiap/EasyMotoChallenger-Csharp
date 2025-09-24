using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Application.Localizacoes.Contracts;
using EasyMoto.Application.Shared.Pagination;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Localizacoes
{
    public sealed class ListarLocalizacoesHandler
    {
        private readonly ILocalizacaoRepository _repo;

        public ListarLocalizacoesHandler(ILocalizacaoRepository repo)
        {
            _repo = repo;
        }

        public async Task<PagedResult<LocalizacaoResponse>> ExecuteAsync(PageQuery query, CancellationToken ct)
        {
            var total = await _repo.CountAsync(ct);
            var items = await _repo.ListAsync(query.Page, query.Size, ct);
            var mapped = items.Select(x => new LocalizacaoResponse
            {
                Id = x.Id,
                StatusLoc = x.StatusLoc,
                DataHora = x.DataHora,
                ZonaVirtual = x.ZonaVirtual,
                Latitude = x.Latitude,
                Longitude = x.Longitude
            });
            return new PagedResult<LocalizacaoResponse>(mapped, query.Page, query.Size, total);
        }
    }
}