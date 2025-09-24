using System;
using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Application.Localizacoes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Localizacoes
{
    public sealed class AtualizarLocalizacaoHandler
    {
        private readonly ILocalizacaoRepository _repo;

        public AtualizarLocalizacaoHandler(ILocalizacaoRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> ExecuteAsync(Guid id, AtualizarLocalizacaoRequest req, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity == null) return false;
            entity.Update(req.StatusLoc, req.DataHora, req.ZonaVirtual, req.Latitude, req.Longitude);
            await _repo.UpdateAsync(entity, ct);
            return true;
        }
    }
}