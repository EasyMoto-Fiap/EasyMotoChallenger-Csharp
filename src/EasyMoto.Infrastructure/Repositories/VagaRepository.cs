using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories
{
    public class VagaRepository : IVagaRepository
    {
        private readonly AppDbContext _ctx;

        public VagaRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(Vaga entity, CancellationToken ct)
        {
            await _ctx.Set<Vaga>().AddAsync(entity, ct);
            await _ctx.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Vaga entity, CancellationToken ct)
        {
            _ctx.Set<Vaga>().Update(entity);
            await _ctx.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var entity = await _ctx.Set<Vaga>().FirstOrDefaultAsync(x => x.Id == id, ct);
            if (entity != null)
            {
                _ctx.Set<Vaga>().Remove(entity);
                await _ctx.SaveChangesAsync(ct);
            }
        }

        public Task<Vaga?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return _ctx.Set<Vaga>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<List<Vaga>> ListAsync(int page, int size, CancellationToken ct)
        {
            return await _ctx.Set<Vaga>()
                .AsNoTracking()
                .OrderBy(x => x.NumeroVaga)
                .ThenBy(x => x.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(ct);
        }

        public Task<long> CountAsync(CancellationToken ct)
        {
            return _ctx.Set<Vaga>().LongCountAsync(ct);
        }
    }
}