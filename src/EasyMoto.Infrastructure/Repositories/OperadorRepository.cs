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
    public class OperadorRepository : IOperadorRepository
    {
        private readonly AppDbContext _db;

        public OperadorRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Operador entity, CancellationToken ct = default)
        {
            await _db.Operadores.AddAsync(entity, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Operador entity, CancellationToken ct = default)
        {
            _db.Operadores.Update(entity);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _db.Operadores.FirstOrDefaultAsync(o => o.Id == id, ct);
            if (entity == null) return;
            _db.Operadores.Remove(entity);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<Operador?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _db.Operadores.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id, ct);
        }

        public async Task<IReadOnlyList<Operador>> ListAsync(int page, int size, CancellationToken ct = default)
        {
            if (page < 1) page = 1;
            if (size < 1) size = 10;
            return await _db.Operadores
                .AsNoTracking()
                .OrderBy(o => o.IdOperador)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(ct);
        }

        public async Task<int> CountAsync(CancellationToken ct = default)
        {
            return await _db.Operadores.AsNoTracking().CountAsync(ct);
        }
    }
}
