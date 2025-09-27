using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories;

public sealed class MotoRepository : IMotoRepository
{
    private readonly AppDbContext _db;

    public MotoRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Moto moto, CancellationToken ct)
    {
        _db.Set<Moto>().Add(moto);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Moto moto, CancellationToken ct)
    {
        _db.Set<Moto>().Update(moto);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        var entity = await _db.Set<Moto>().FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return;
        _db.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }

    public Task<Moto?> GetByIdAsync(int id, CancellationToken ct) =>
        _db.Set<Moto>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<Moto>> ListAsync(int page, int size, CancellationToken ct) =>
        await _db.Set<Moto>()
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(ct);

    public Task<int> CountAsync(CancellationToken ct) =>
        _db.Set<Moto>().CountAsync(ct);

    public Task<bool> ExistsPlacaAsync(string placa, CancellationToken ct) =>
        _db.Set<Moto>().AnyAsync(x => x.Placa == placa, ct);
}