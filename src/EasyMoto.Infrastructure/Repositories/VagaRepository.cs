using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories;

public sealed class VagaRepository : IVagaRepository
{
    private readonly AppDbContext _db;

    public VagaRepository(AppDbContext db) => _db = db;

    public Task<int> CountAsync(CancellationToken ct) =>
        _db.Vagas.AsNoTracking().CountAsync(ct);

    public async Task<IReadOnlyList<Vaga>> ListAsync(int page, int size, CancellationToken ct) =>
        await _db.Vagas.AsNoTracking()
            .OrderBy(v => v.PatioId).ThenBy(v => v.NumeroVaga)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(ct);

    public Task<Vaga?> GetByIdAsync(int id, CancellationToken ct) =>
        _db.Vagas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id, ct);

    public async Task AddAsync(Vaga entity, CancellationToken ct)
    {
        await _db.Vagas.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Vaga entity, CancellationToken ct)
    {
        _db.Vagas.Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        var e = await _db.Vagas.FindAsync(new object[] { id }, ct);
        if (e is null) return;
        _db.Vagas.Remove(e);
        await _db.SaveChangesAsync(ct);
    }

    public Task<bool> ExistsNumeroAsync(int patioId, string numeroVaga, int? ignoreId, CancellationToken ct) =>
        _db.Vagas.AsNoTracking()
            .AnyAsync(v =>
                v.PatioId == patioId &&
                v.NumeroVaga == numeroVaga &&
                (ignoreId == null || v.Id != ignoreId), ct);
}