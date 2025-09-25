using EasyMoto.Application.Patios.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Patios
{
    public class ObterPatioPorIdHandler
    {
        private readonly IPatioRepository _repo;

        public ObterPatioPorIdHandler(IPatioRepository repo)
        {
            _repo = repo;
        }

        public async Task<PatioResponse?> ExecuteAsync(Guid id, CancellationToken ct)
        {
            var e = await _repo.GetByIdAsync(id, ct);
            if (e == null) return null;
            return new PatioResponse
            {
                Id = e.Id,
                NomePatio = e.NomePatio,
                TamanhoPatio = e.TamanhoPatio,
                Andar = e.Andar,
                FilialId = e.FilialId
            };
        }
    }
}