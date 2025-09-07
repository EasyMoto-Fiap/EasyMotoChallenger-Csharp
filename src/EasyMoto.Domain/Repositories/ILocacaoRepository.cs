using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories;

public interface ILocacaoRepository
{
    Task<Locacao?> GetByIdAsync(int id, CancellationToken ct);
    Task<List<Locacao>> GetAllAsync(CancellationToken ct);
    Task AddAsync(Locacao locacao, CancellationToken ct);
    Task UpdateAsync(Locacao locacao, CancellationToken ct);
    Task DeleteAsync(int id, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}
