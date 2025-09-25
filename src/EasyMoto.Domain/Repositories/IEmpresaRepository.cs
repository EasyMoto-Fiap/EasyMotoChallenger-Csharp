using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories;

public interface IEmpresaRepository
{
    Task<Empresa> AddAsync(Empresa entity, CancellationToken ct);
    Task<Empresa?> GetByIdAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<Empresa>> ListAsync(int page, int size, CancellationToken ct);
    Task<long> CountAsync(CancellationToken ct);
    Task<Empresa?> UpdateAsync(int id, Empresa input, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}