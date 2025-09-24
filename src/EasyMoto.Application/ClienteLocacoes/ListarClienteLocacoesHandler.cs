using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Application.ClienteLocacoes.Contracts;
using EasyMoto.Application.Shared.Pagination;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.ClienteLocacoes
{
    public sealed class ListarClienteLocacoesHandler
    {
        private readonly IClienteLocacaoRepository _repo;

        public ListarClienteLocacoesHandler(IClienteLocacaoRepository repo) => _repo = repo;

        public async Task<PagedResult<ClienteLocacaoResponse>> ExecuteAsync(PageQuery query, CancellationToken ct = default)
        {
            var total = await _repo.CountAsync(ct);
            var items = await _repo.ListAsync(query.Page, query.Size, ct);

            var result = items.Select(e => new ClienteLocacaoResponse
            {
                Id = e.Id,
                ClienteId = e.ClienteId,
                MotoId = e.MotoId,
                DataInicio = e.Periodo.Inicio,
                DataFim = e.Periodo.Fim,
                StatusLocacao = e.StatusLocacao
            });

            return new PagedResult<ClienteLocacaoResponse>(result, query.Page, query.Size, total);
        }
    }
}