using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories;

public interface IClienteRepository
{
    Task<Cliente?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Cliente entity, CancellationToken ct = default);
    Task UpdateAsync(Cliente entity, CancellationToken ct = default);
    Task DeleteAsync(Cliente entity, CancellationToken ct = default);
    Task<List<Cliente>> ListAsync(int page, int size, CancellationToken ct = default);
    Task<long> CountAsync(CancellationToken ct = default);
}