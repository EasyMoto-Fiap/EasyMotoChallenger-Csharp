using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories
{
    public sealed class FilialRepository : IFilialRepository
    {
        private readonly AppDbContext _db;
        public FilialRepository(AppDbContext db) => _db = db;

        public Task<long> CountAsync(CancellationToken ct = default) =>
            _db.Filiais.LongCountAsync(ct);

        public async Task<IReadOnlyList<Filial>> ListAsync(int page, int size, CancellationToken ct = default)
        {
            if (page < 1) page = 1;
            if (size < 1) size = 10;
            return await _db.Filiais.AsNoTracking()
                .OrderBy(f => f.IdFilial)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(ct);
        }

        public Task<Filial?> GetByIdAsync(int id, CancellationToken ct = default) =>
            _db.Filiais.AsNoTracking().FirstOrDefaultAsync(f => f.IdFilial == id, ct);

        public async Task<Filial> AddAsync(Filial entity, CancellationToken ct = default)
        {
            _db.Filiais.Add(entity);
            await _db.SaveChangesAsync(ct);
            return entity;
        }

        public async Task<Filial?> UpdateAsync(int id, Filial input, CancellationToken ct = default)
        {
            var current = await _db.Filiais.FirstOrDefaultAsync(f => f.IdFilial == id, ct);
            if (current == null) return null;
            current.Update(input.NomeFilial, input.Cidade, input.Estado, input.Pais, input.Endereco, input.EmpresaId);
            await _db.SaveChangesAsync(ct);
            return current;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var current = await _db.Filiais.FirstOrDefaultAsync(f => f.IdFilial == id, ct);
            if (current == null) return false;
            _db.Filiais.Remove(current);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
