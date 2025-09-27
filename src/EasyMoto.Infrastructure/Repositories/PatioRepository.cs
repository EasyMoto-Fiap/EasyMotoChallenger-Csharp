using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories
{
    public sealed class PatioRepository : IPatioRepository
    {
        private readonly AppDbContext _db;

        public PatioRepository(AppDbContext db) => _db = db;

        public async Task AddAsync(Patio patio, CancellationToken ct = default)
        {
            _db.Set<Patio>().Add(patio);
            await _db.SaveChangesAsync(ct);
        }

        public Task<Patio?> GetByIdAsync(int id, CancellationToken ct = default) =>
            _db.Set<Patio>().AsNoTracking().FirstOrDefaultAsync(p => p.IdPatio == id, ct);

        public async Task<bool> UpdateAsync(Patio patio, CancellationToken ct = default)
        {
            _db.Set<Patio>().Update(patio);
            await _db.SaveChangesAsync(ct);
            return true;
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _db.Set<Patio>().FirstOrDefaultAsync(p => p.IdPatio == id, ct);
            if (entity is null) return;
            _db.Remove(entity);
            await _db.SaveChangesAsync(ct);
        }

        public Task<int> CountAsync(CancellationToken ct = default) =>
            _db.Set<Patio>().CountAsync(ct);

        public async Task<IReadOnlyList<Patio>> ListAsync(int page, int size, CancellationToken ct = default)
        {
            var list = await _db.Set<Patio>()
                .AsNoTracking()
                .OrderBy(p => p.NomePatio)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(ct);
            return list;
        }

        public async Task<IReadOnlyList<Patio>> ListByFilialAsync(int filialId, CancellationToken ct = default)
        {
            var list = await _db.Set<Patio>()
                .AsNoTracking()
                .Where(p => p.FilialId == filialId)
                .OrderBy(p => p.NomePatio)
                .ToListAsync(ct);
            return list;
        }
    }
}
