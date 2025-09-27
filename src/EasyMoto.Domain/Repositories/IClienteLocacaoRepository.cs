using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories;

public interface IClienteLocacaoRepository
{
    Task<int> CountAsync(CancellationToken ct = default);
    Task<List<ClienteLocacao>> ListAsync(int page, int size, CancellationToken ct = default);
    Task<ClienteLocacao?> GetByIdAsync(int id, CancellationToken ct = default);
    Task AddAsync(ClienteLocacao entity, CancellationToken ct = default);
    Task UpdateAsync(ClienteLocacao entity, CancellationToken ct = default);
    Task DeleteAsync(ClienteLocacao entity, CancellationToken ct = default);
}