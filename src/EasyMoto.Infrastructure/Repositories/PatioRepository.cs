using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories;

public sealed class PatioRepository : IPatioRepository
{
    private readonly AppDbContext _db;
    public PatioRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Patio entity, CancellationToken ct = default)
    {
        await _db.Set<Patio>().AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<Patio?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _db.Set<Patio>().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task UpdateAsync(Patio entity, CancellationToken ct = default)
    {
        _db.Set<Patio>().Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var e = await GetByIdAsync(id, ct);
        if (e is null) return;
        _db.Set<Patio>().Remove(e);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<Patio>> ListAsync(CancellationToken ct = default)
        => await _db.Set<Patio>().AsNoTracking().ToListAsync(ct);
}