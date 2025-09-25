using EasyMoto.Application.Vagas.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Vagas
{
    public class ObterVagaPorIdHandler
    {
        private readonly IVagaRepository _repo;

        public ObterVagaPorIdHandler(IVagaRepository repo)
        {
            _repo = repo;
        }

        public async Task<VagaResponse?> ExecuteAsync(Guid id, CancellationToken ct)
        {
            var e = await _repo.GetByIdAsync(id, ct);
            if (e == null) return null;
            return new VagaResponse
            {
                Id = e.Id,
                NumeroVaga = e.NumeroVaga,
                StatusVaga = e.StatusVaga,
                MotoId = e.MotoId,
                PatioId = e.PatioId
            };
        }
    }
}