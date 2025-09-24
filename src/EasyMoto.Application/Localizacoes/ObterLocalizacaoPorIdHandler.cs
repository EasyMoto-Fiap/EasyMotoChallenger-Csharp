using System;
using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Application.Localizacoes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Localizacoes
{
    public sealed class ObterLocalizacaoPorIdHandler
    {
        private readonly ILocalizacaoRepository _repo;

        public ObterLocalizacaoPorIdHandler(ILocalizacaoRepository repo)
        {
            _repo = repo;
        }

        public async Task<LocalizacaoResponse?> ExecuteAsync(Guid id, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity == null) return null;
            return new LocalizacaoResponse
            {
                Id = entity.Id,
                StatusLoc = entity.StatusLoc,
                DataHora = entity.DataHora,
                ZonaVirtual = entity.ZonaVirtual,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude
            };
        }
    }
}