using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories;

public interface IMotoRepository
{
    Task AddAsync(Moto moto, CancellationToken ct);
    Task UpdateAsync(Moto moto, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);

    Task<Moto?> GetByIdAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<Moto>> ListAsync(int page, int size, CancellationToken ct);
    Task<int> CountAsync(CancellationToken ct);

    Task<bool> ExistsPlacaAsync(string placa, CancellationToken ct);
}