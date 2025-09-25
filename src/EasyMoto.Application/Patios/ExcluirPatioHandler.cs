using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Patios
{
    public class ExcluirPatioHandler
    {
        private readonly IPatioRepository _repo;

        public ExcluirPatioHandler(IPatioRepository repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, CancellationToken ct)
        {
            await _repo.DeleteAsync(id, ct);
        }
    }
}