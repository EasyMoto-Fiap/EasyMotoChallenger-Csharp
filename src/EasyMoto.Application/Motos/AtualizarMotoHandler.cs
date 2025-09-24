using System;
using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Application.Motos.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Motos
{
    public sealed class AtualizarMotoHandler
    {
        private readonly IMotoRepository _repo;
        public AtualizarMotoHandler(IMotoRepository repo) => _repo = repo;

        public async Task ExecuteAsync(Guid id, AtualizarMotoRequest req, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) throw new KeyNotFoundException("Moto não encontrada.");
            entity.Update(req.Placa, req.Modelo, req.AnoFabricacao, req.Status, req.LocacaoId, req.LocalizacaoId);
            await _repo.UpdateAsync(entity, ct);
        }
    }
}