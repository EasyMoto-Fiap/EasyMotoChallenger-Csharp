using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories
{
    public interface IPatioRepository
    {
        Task AddAsync(Patio patio, CancellationToken ct = default);
        Task<Patio?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<bool> UpdateAsync(Patio patio, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
        Task<int> CountAsync(CancellationToken ct = default);
        Task<IReadOnlyList<Patio>> ListAsync(int page, int size, CancellationToken ct = default);
        Task<IReadOnlyList<Patio>> ListByFilialAsync(int filialId, CancellationToken ct = default);
    }
}