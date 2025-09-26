using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories
{
    public interface IOperadorRepository
    {
        Task AddAsync(Operador entity, CancellationToken ct = default);
        Task UpdateAsync(Operador entity, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
        Task<Operador?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<Operador>> ListAsync(int page, int size, CancellationToken ct = default);
        Task<int> CountAsync(CancellationToken ct = default);
    }
}