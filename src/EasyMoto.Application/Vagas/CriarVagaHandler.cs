using EasyMoto.Application.Vagas.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Vagas
{
    public class CriarVagaHandler
    {
        private readonly IVagaRepository _repo;

        public CriarVagaHandler(IVagaRepository repo)
        {
            _repo = repo;
        }

        public async Task<VagaResponse> ExecuteAsync(CriarVagaRequest req, CancellationToken ct)
        {
            var entity = new Vaga(Guid.NewGuid(), req.NumeroVaga, req.StatusVaga, req.MotoId, req.PatioId);
            await _repo.AddAsync(entity, ct);
            return new VagaResponse
            {
                Id = entity.Id,
                NumeroVaga = entity.NumeroVaga,
                StatusVaga = entity.StatusVaga,
                MotoId = entity.MotoId,
                PatioId = entity.PatioId
            };
        }
    }
}