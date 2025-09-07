using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories;

public interface IClienteRepository
{
    Task<Cliente?> GetByIdAsync(int id, CancellationToken ct);
    Task AddAsync(Cliente cliente, CancellationToken ct);
    Task UpdateAsync(Cliente cliente, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
    Task<bool> ExistsByCpfAsync(string cpf, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}
