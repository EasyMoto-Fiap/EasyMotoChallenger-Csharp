using EasyMoto.Application.Patios.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Patios
{
    public class CriarPatioHandler
    {
        private readonly IPatioRepository _repo;

        public CriarPatioHandler(IPatioRepository repo)
        {
            _repo = repo;
        }

        public async Task<PatioResponse> ExecuteAsync(CriarPatioRequest req, CancellationToken ct)
        {
            var entity = new Patio(Guid.NewGuid(), req.NomePatio, req.TamanhoPatio, req.Andar, req.FilialId);
            await _repo.AddAsync(entity, ct);
            return new PatioResponse
            {
                Id = entity.Id,
                NomePatio = entity.NomePatio,
                TamanhoPatio = entity.TamanhoPatio,
                Andar = entity.Andar,
                FilialId = entity.FilialId
            };
        }
    }
}