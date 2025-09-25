using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories
{
    public interface IVagaRepository
    {
        Task AddAsync(Vaga entity, CancellationToken ct);
        Task UpdateAsync(Vaga entity, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
        Task<Vaga?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<List<Vaga>> ListAsync(int page, int size, CancellationToken ct);
        Task<long> CountAsync(CancellationToken ct);
    }
}