using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories;

public sealed class MotoRepository : IMotoRepository
{
    private readonly AppDbContext _db;

    public MotoRepository(AppDbContext db) => _db = db;

    public Task<Moto?> GetByIdAsync(int id, CancellationToken ct) =>
        _db.Motos.FirstOrDefaultAsync(m => m.IdMoto == id, ct);

    public Task<List<Moto>> GetAllAsync(CancellationToken ct) =>
        _db.Motos.AsNoTracking().OrderBy(m => m.IdMoto).ToListAsync(ct);

    public async Task AddAsync(Moto moto, CancellationToken ct)
    {
        await _db.Motos.AddAsync(moto, ct);
    }

    public Task UpdateAsync(Moto moto, CancellationToken ct)
    {
        _db.Motos.Update(moto);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        var entity = await _db.Motos.FirstOrDefaultAsync(m => m.IdMoto == id, ct);
        if (entity is not null) _db.Motos.Remove(entity);
    }

    public async Task<bool> ExistsByPlacaAsync(string placa, CancellationToken ct)
    {
        var p = (placa ?? string.Empty).Trim().ToUpperInvariant();
        var count = await _db.Motos.CountAsync(m => m.Placa == p, ct);
        return count > 0;
    }

    public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
}
