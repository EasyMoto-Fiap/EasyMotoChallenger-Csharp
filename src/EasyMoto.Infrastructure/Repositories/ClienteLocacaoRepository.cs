using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories
{
    public sealed class ClienteLocacaoRepository : IClienteLocacaoRepository
    {
        private readonly AppDbContext _db;
        public ClienteLocacaoRepository(AppDbContext db) => _db = db;

        public async Task AddAsync(ClienteLocacao entity, CancellationToken ct = default)
        {
            await _db.Set<ClienteLocacao>().AddAsync(entity, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(ClienteLocacao entity, CancellationToken ct = default)
        {
            _db.Set<ClienteLocacao>().Update(entity);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(ClienteLocacao entity, CancellationToken ct = default)
        {
            _db.Set<ClienteLocacao>().Remove(entity);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<ClienteLocacao?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _db.Set<ClienteLocacao>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<List<ClienteLocacao>> ListAsync(int page, int size, CancellationToken ct = default)
        {
            return await _db.Set<ClienteLocacao>()
                .AsNoTracking()
                .OrderBy(x => x.Periodo.Inicio)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(ct);
        }

        public async Task<int> CountAsync(CancellationToken ct = default)
        {
            return await _db.Set<ClienteLocacao>().CountAsync(ct);
        }
    }
}