using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories
{
    public class OperadorRepository : IOperadorRepository
    {
        private readonly AppDbContext _context;

        public OperadorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Operador entity, CancellationToken ct)
        {
            await _context.Set<Operador>().AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<Operador?> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Set<Operador>()
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyList<Operador>> ListAsync(int page, int size, CancellationToken ct)
        {
            return await _context.Set<Operador>()
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(ct);
        }

        public async Task<int> CountAsync(CancellationToken ct)
        {
            return await _context.Set<Operador>().CountAsync(ct);
        }

        public async Task UpdateAsync(Operador entity, CancellationToken ct)
        {
            _context.Set<Operador>().Update(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            var entity = await _context.Set<Operador>()
                .FirstOrDefaultAsync(x => x.Id == id, ct);

            if (entity != null)
            {
                _context.Set<Operador>().Remove(entity);
                await _context.SaveChangesAsync(ct);
            }
        }
    }
}