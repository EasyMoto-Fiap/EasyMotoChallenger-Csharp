using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Application.Motos.Contracts;
using EasyMoto.Application.Shared.Pagination;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Motos
{
    public sealed class ListarMotosHandler
    {
        private readonly IMotoRepository _repo;
        public ListarMotosHandler(IMotoRepository repo) => _repo = repo;

        public async Task<PagedResult<MotoResponse>> ExecuteAsync(PageQuery query, CancellationToken ct = default)
        {
            var total = await _repo.CountAsync(ct);
            var items = await _repo.ListAsync(query.Page, query.Size, ct);
            var result = items.Select(m => new MotoResponse
            {
                Id = m.Id,
                Placa = m.Placa,
                Modelo = m.Modelo,
                AnoFabricacao = m.AnoFabricacao,
                Status = m.Status,
                LocacaoId = m.LocacaoId,
                LocalizacaoId = m.LocalizacaoId
            });
            return new PagedResult<MotoResponse>(result, query.Page, query.Size, total);
        }
    }
}