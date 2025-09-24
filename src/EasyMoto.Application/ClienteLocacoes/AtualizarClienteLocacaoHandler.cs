using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Application.ClienteLocacoes.Contracts;
using EasyMoto.Domain.Repositories;
using EasyMoto.Domain.ValueObjects;

namespace EasyMoto.Application.ClienteLocacoes
{
    public sealed class AtualizarClienteLocacaoHandler
    {
        private readonly IClienteLocacaoRepository _repo;
        public AtualizarClienteLocacaoHandler(IClienteLocacaoRepository repo) => _repo = repo;

        public async Task ExecuteAsync(System.Guid id, AtualizarClienteLocacaoRequest request, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) throw new KeyNotFoundException("Locação não encontrada.");
            var periodo = new Periodo(request.DataInicio, request.DataFim);
            entity.Update(request.ClienteId, request.MotoId, periodo, request.StatusLocacao);
            await _repo.UpdateAsync(entity, ct);
        }
    }
}