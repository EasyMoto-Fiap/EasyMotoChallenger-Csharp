using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories
{
    public interface IOperadorRepository
    {
        Task<Operador> AddAsync(Operador entity, CancellationToken ct);
        Task<Operador?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<List<Operador>> ListAsync(int page, int size, CancellationToken ct);
        Task UpdateAsync(Operador entity, CancellationToken ct);
        Task DeleteAsync(Guid id, CancellationToken ct);
    }
}