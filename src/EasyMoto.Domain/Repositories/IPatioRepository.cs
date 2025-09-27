using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories;

public interface IPatioRepository
{
    Task AddAsync(Patio entity, CancellationToken ct = default);
    Task<Patio?> GetByIdAsync(int id, CancellationToken ct = default);
    Task UpdateAsync(Patio entity, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Patio>> ListAsync(CancellationToken ct = default);
}