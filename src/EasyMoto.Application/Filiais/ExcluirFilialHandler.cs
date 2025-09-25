using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Filiais
{
    public sealed class ExcluirFilialHandler
    {
        private readonly IFilialRepository _repo;
        public ExcluirFilialHandler(IFilialRepository repo) => _repo = repo;
        public Task<bool> ExecuteAsync(int id, CancellationToken ct = default) => _repo.DeleteAsync(id, ct);
    }
}