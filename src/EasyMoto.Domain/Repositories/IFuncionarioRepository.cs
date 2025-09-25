using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories
{
    public interface IFuncionarioRepository
    {
        Task<Funcionario?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<IReadOnlyList<Funcionario>> ListAsync(int page, int size, CancellationToken ct);
        Task<long> CountAsync(CancellationToken ct);
        Task<Funcionario> AddAsync(Funcionario entity, CancellationToken ct);
        Task<Funcionario?> UpdateAsync(Funcionario entity, CancellationToken ct);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct);
    }
}