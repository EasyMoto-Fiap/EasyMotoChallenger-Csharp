using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Application.ClienteLocacoes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.ClienteLocacoes
{
    public sealed class ObterClienteLocacaoPorIdHandler
    {
        private readonly IClienteLocacaoRepository _repo;
        public ObterClienteLocacaoPorIdHandler(IClienteLocacaoRepository repo) => _repo = repo;

        public async Task<ClienteLocacaoResponse?> ExecuteAsync(System.Guid id, CancellationToken ct = default)
        {
            var e = await _repo.GetByIdAsync(id, ct);
            if (e is null) return null;
            return new ClienteLocacaoResponse
            {
                Id = e.Id,
                ClienteId = e.ClienteId,
                MotoId = e.MotoId,
                DataInicio = e.Periodo.Inicio,
                DataFim = e.Periodo.Fim,
                StatusLocacao = e.StatusLocacao
            };
        }
    }
}