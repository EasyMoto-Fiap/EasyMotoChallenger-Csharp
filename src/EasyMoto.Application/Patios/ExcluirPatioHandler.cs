using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Patios
{
    public sealed class ExcluirPatioHandler
    {
        private readonly IPatioRepository _repo;

        public ExcluirPatioHandler(IPatioRepository repo) => _repo = repo;

        public Task ExecuteAsync(int id, CancellationToken ct) => _repo.DeleteAsync(id, ct);
    }
}