using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Operadores
{
    public class ExcluirOperadorHandler
    {
        private readonly IOperadorRepository _repo;

        public ExcluirOperadorHandler(IOperadorRepository repo)
        {
            _repo = repo;
        }

        public async Task ExecuteAsync(Guid id, CancellationToken ct)
        {
            await _repo.DeleteAsync(id, ct);
        }
    }
} 