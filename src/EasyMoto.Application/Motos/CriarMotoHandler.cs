using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Application.Motos.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Motos
{
    public sealed class CriarMotoHandler
    {
        private readonly IMotoRepository _repo;
        public CriarMotoHandler(IMotoRepository repo) => _repo = repo;

        public async Task<MotoResponse> ExecuteAsync(CriarMotoRequest req, CancellationToken ct = default)
        {
            var entity = new Moto(req.Placa, req.Modelo, req.AnoFabricacao, req.Status, req.LocacaoId, req.LocalizacaoId);
            await _repo.AddAsync(entity, ct);
            return new MotoResponse
            {
                Id = entity.Id,
                Placa = entity.Placa,
                Modelo = entity.Modelo,
                AnoFabricacao = entity.AnoFabricacao,
                Status = entity.Status,
                LocacaoId = entity.LocacaoId,
                LocalizacaoId = entity.LocalizacaoId
            };
        }
    }
}