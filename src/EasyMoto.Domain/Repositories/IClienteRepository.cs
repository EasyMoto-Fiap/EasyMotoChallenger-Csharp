using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories;

public interface IClienteRepository
{
    Task<int> CountAsync(CancellationToken ct);
    Task<IReadOnlyList<Cliente>> ListAsync(int page, int size, CancellationToken ct);
    Task<Cliente?> GetByIdAsync(int id, CancellationToken ct);
    Task AddAsync(Cliente entity, CancellationToken ct);
    Task UpdateAsync(Cliente entity, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
    Task<bool> ExistsCpfAsync(string cpf, int? ignoreId, CancellationToken ct);
}