using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories;

public interface ILocalizacaoRepository
{
    Task<int> CountAsync(CancellationToken ct);
    Task<IReadOnlyList<Localizacao>> ListAsync(int page, int size, CancellationToken ct);
    Task<Localizacao?> GetByIdAsync(int id, CancellationToken ct);
    Task AddAsync(Localizacao entity, CancellationToken ct);
    Task UpdateAsync(Localizacao entity, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
}