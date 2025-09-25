using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories
{
    public class PatioRepository : IPatioRepository
    {
        private readonly AppDbContext _ctx;

        public PatioRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(Patio entity, CancellationToken ct)
        {
            await _ctx.Set<Patio>().AddAsync(entity, ct);
            await _ctx.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Patio entity, CancellationToken ct)
        {
            _ctx.Set<Patio>().Update(entity);
            await _ctx.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var entity = await _ctx.Set<Patio>().FirstOrDefaultAsync(x => x.Id == id, ct);
            if (entity != null)
            {
                _ctx.Set<Patio>().Remove(entity);
                await _ctx.SaveChangesAsync(ct);
            }
        }

        public Task<Patio?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return _ctx.Set<Patio>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<List<Patio>> ListAsync(int page, int size, CancellationToken ct)
        {
            return await _ctx.Set<Patio>()
                .AsNoTracking()
                .OrderBy(x => x.NomePatio)
                .ThenBy(x => x.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(ct);
        }

        public Task<long> CountAsync(CancellationToken ct)
        {
            return _ctx.Set<Patio>().LongCountAsync(ct);
        }
    }
}