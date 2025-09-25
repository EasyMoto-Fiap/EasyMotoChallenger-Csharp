using EasyMoto.Application.Vagas.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Vagas
{
    public class AtualizarVagaHandler
    {
        private readonly IVagaRepository _repo;

        public AtualizarVagaHandler(IVagaRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> ExecuteAsync(Guid id, AtualizarVagaRequest req, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity == null) return false;

            entity.Update(req.NumeroVaga, req.StatusVaga, req.MotoId, req.PatioId);
            await _repo.UpdateAsync(entity, ct);
            return true;
        }
    }
}