using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories;

public interface IFuncionarioRepository
{
    Task<Funcionario?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<List<Funcionario>> ListAsync(int page, int size, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
    Task AddAsync(Funcionario entity, CancellationToken ct = default);
    Task UpdateAsync(Funcionario entity, CancellationToken ct = default);
    Task DeleteAsync(Funcionario entity, CancellationToken ct = default);
}