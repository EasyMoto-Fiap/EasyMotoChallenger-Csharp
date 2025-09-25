using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories
{
    public interface IPatioRepository
    {
        Task AddAsync(Patio entity, CancellationToken ct);
        Task UpdateAsync(Patio entity, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
        Task<Patio?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<List<Patio>> ListAsync(int page, int size, CancellationToken ct);
        Task<long> CountAsync(CancellationToken ct);
    }
}