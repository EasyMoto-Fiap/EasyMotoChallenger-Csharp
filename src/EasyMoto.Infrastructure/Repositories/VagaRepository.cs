using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories;

public sealed class VagaRepository : IVagaRepository
{
    private readonly AppDbContext _db;

    public VagaRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Vaga entity, CancellationToken ct)
    {
        _db.Set<Vaga>().Add(entity);
        await _db.SaveChangesAsync(ct);
    }

    public Task<Vaga?> GetByIdAsync(int id, CancellationToken ct) =>
        _db.Set<Vaga>().FirstOrDefaultAsync(v => v.Id == id, ct);

    public async Task UpdateAsync(Vaga entity, CancellationToken ct)
    {
        _db.Set<Vaga>().Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        var entity = await GetByIdAsync(id, ct);
        if (entity is null) return;
        _db.Set<Vaga>().Remove(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<Vaga>> ListAsync(int page, int size, CancellationToken ct) =>
        await _db.Set<Vaga>()
            .AsNoTracking()
            .OrderBy(v => v.PatioId).ThenBy(v => v.NumeroVaga)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(ct);

    public Task<int> CountAsync(CancellationToken ct) =>
        _db.Set<Vaga>().CountAsync(ct);

    public Task<bool> ExistsNumeroAsync(int patioId, int numeroVaga, int? ignoreId, CancellationToken ct) =>
        _db.Set<Vaga>().AnyAsync(v =>
            v.PatioId == patioId &&
            v.NumeroVaga == numeroVaga &&
            (ignoreId == null || v.Id != ignoreId), ct);
}