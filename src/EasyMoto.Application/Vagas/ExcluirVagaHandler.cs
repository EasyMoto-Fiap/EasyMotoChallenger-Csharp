using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Vagas
{
    public class ExcluirVagaHandler
    {
        private readonly IVagaRepository _repo;

        public ExcluirVagaHandler(IVagaRepository repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, CancellationToken ct)
        {
            await _repo.DeleteAsync(id, ct);
        }
    }
}