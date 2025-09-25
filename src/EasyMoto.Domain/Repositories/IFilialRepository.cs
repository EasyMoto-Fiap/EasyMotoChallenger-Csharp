using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories
{
    public interface IFilialRepository
    {
        Task<long> CountAsync(CancellationToken ct = default);
        Task<IReadOnlyList<Filial>> ListAsync(int page, int size, CancellationToken ct = default);
        Task<Filial?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Filial> AddAsync(Filial entity, CancellationToken ct = default);
        Task<Filial?> UpdateAsync(int id, Filial input, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}