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
    public class LocalizacaoRepository : ILocalizacaoRepository
    {
        private readonly AppDbContext _db;

        public LocalizacaoRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Localizacao?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _db.Set<Localizacao>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<List<Localizacao>> ListAsync(int page, int size, CancellationToken ct)
        {
            return await _db.Set<Localizacao>()
                .AsNoTracking()
                .OrderBy(x => x.DataHora)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(ct);
        }

        public async Task<int> CountAsync(CancellationToken ct)
        {
            return await _db.Set<Localizacao>().CountAsync(ct);
        }

        public async Task AddAsync(Localizacao entity, CancellationToken ct)
        {
            await _db.Set<Localizacao>().AddAsync(entity, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Localizacao entity, CancellationToken ct)
        {
            _db.Set<Localizacao>().Update(entity);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Localizacao entity, CancellationToken ct)
        {
            _db.Set<Localizacao>().Remove(entity);
            await _db.SaveChangesAsync(ct);
        }
    }
}