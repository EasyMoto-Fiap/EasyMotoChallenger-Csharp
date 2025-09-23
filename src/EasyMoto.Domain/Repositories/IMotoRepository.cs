using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories;

public interface IMotoRepository
{
    Task<Moto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Moto entity, CancellationToken ct = default);
    Task UpdateAsync(Moto entity, CancellationToken ct = default);
    Task DeleteAsync(Moto entity, CancellationToken ct = default);
    Task<List<Moto>> ListAsync(int page, int size, CancellationToken ct = default);
    Task<long> CountAsync(CancellationToken ct = default);
}