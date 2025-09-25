using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories
{
    public class OperadorRepository : IOperadorRepository
    {
        private readonly AppDbContext _ctx;

        public OperadorRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Operador> AddAsync(Operador entity, CancellationToken ct)
        {
            _ctx.Set<Operador>().Add(entity);
            await _ctx.SaveChangesAsync(ct);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var entity = await _ctx.Set<Operador>().FirstOrDefaultAsync(x => x.Id == id, ct);
            if (entity == null) return;
            _ctx.Set<Operador>().Remove(entity);
            await _ctx.SaveChangesAsync(ct);
        }

        public async Task<Operador?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _ctx.Set<Operador>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<List<Operador>> ListAsync(int page, int size, CancellationToken ct)
        {
            return await _ctx.Set<Operador>()
                .OrderBy(x => x.NomeOperador)
                .Skip((page - 1) * size)
                .Take(size)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task UpdateAsync(Operador entity, CancellationToken ct)
        {
            _ctx.Set<Operador>().Update(entity);
            await _ctx.SaveChangesAsync(ct);
        }
    }
}