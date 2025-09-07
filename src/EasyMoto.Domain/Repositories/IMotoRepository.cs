using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories;

public interface IMotoRepository
{
    Task<Moto?> GetByIdAsync(int id, CancellationToken ct);
    Task<List<Moto>> GetAllAsync(CancellationToken ct);
    Task AddAsync(Moto moto, CancellationToken ct);
    Task UpdateAsync(Moto moto, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
    Task<bool> ExistsByPlacaAsync(string placa, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}
