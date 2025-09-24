using System;
using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Application.Motos.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Motos
{
    public sealed class ObterMotoPorIdHandler
    {
        private readonly IMotoRepository _repo;
        public ObterMotoPorIdHandler(IMotoRepository repo) => _repo = repo;

        public async Task<MotoResponse?> ExecuteAsync(Guid id, CancellationToken ct = default)
        {
            var m = await _repo.GetByIdAsync(id, ct);
            if (m is null) return null;
            return new MotoResponse
            {
                Id = m.Id,
                Placa = m.Placa,
                Modelo = m.Modelo,
                AnoFabricacao = m.AnoFabricacao,
                Status = m.Status,
                LocacaoId = m.LocacaoId,
                LocalizacaoId = m.LocalizacaoId
            };
        }
    }
}