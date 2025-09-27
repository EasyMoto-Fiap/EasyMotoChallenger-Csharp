using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories;

public interface IVagaRepository
{
    Task<Vaga?> GetByIdAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<Vaga>> ListAsync(int page, int size, CancellationToken ct);
    Task<int> CountAsync(CancellationToken ct);
    Task AddAsync(Vaga entity, CancellationToken ct);
    Task UpdateAsync(Vaga entity, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
    Task<bool> ExistsNumeroAsync(int patioId, string numeroVaga, int? ignoreId, CancellationToken ct);
}