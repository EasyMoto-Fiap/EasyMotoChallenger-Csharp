using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Domain.Entities;

namespace EasyMoto.Domain.Repositories
{
    public interface ILocalizacaoRepository
    {
        Task<Localizacao?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<List<Localizacao>> ListAsync(int page, int size, CancellationToken ct);
        Task<int> CountAsync(CancellationToken ct);
        Task AddAsync(Localizacao entity, CancellationToken ct);
        Task UpdateAsync(Localizacao entity, CancellationToken ct);
        Task DeleteAsync(Localizacao entity, CancellationToken ct);
    }
}