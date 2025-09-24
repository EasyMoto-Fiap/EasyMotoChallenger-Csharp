using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Application.ClienteLocacoes.Contracts;
using EasyMoto.Application.Shared.Pagination;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Domain.ValueObjects;

namespace EasyMoto.Application.ClienteLocacoes
{
    public sealed class CriarClienteLocacaoHandler
    {
        private readonly IClienteLocacaoRepository _repo;
        public CriarClienteLocacaoHandler(IClienteLocacaoRepository repo) => _repo = repo;

        public async Task<ClienteLocacaoResponse> ExecuteAsync(CriarClienteLocacaoRequest req, CancellationToken ct = default)
        {
            var periodo = new Periodo(req.DataInicio, req.DataFim);
            var entity = new ClienteLocacao(req.ClienteId, req.MotoId, periodo, req.StatusLocacao);
            await _repo.AddAsync(entity, ct);
            return new ClienteLocacaoResponse
            {
                Id = entity.Id,
                ClienteId = entity.ClienteId,
                MotoId = entity.MotoId,
                DataInicio = entity.Periodo.Inicio,
                DataFim = entity.Periodo.Fim,
                StatusLocacao = entity.StatusLocacao
            };
        }
    }
}