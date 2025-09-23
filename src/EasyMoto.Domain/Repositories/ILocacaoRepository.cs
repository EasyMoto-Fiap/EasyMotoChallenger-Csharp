using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories;

public interface ILocacaoRepository
{
    Task<Locacao?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Locacao entity, CancellationToken ct = default);
    Task UpdateAsync(Locacao entity, CancellationToken ct = default);
    Task DeleteAsync(Locacao entity, CancellationToken ct = default);
    Task<List<Locacao>> ListAsync(int page, int size, CancellationToken ct = default);
    Task<long> CountAsync(CancellationToken ct = default);
}