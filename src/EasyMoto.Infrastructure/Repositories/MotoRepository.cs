using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories;

public sealed class MotoRepository : IMotoRepository
{
    private readonly AppDbContext _db;

    public MotoRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Moto entity, CancellationToken ct = default)
    {
        await _db.Motos.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Moto entity, CancellationToken ct = default)
    {
        _db.Motos.Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Moto entity, CancellationToken ct = default)
    {
        _db.Motos.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }

    public Task<Moto?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        _db.Motos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<List<Moto>> ListAsync(int page, int size, CancellationToken ct = default) =>
        await _db.Motos.AsNoTracking()
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(ct);

    public Task<long> CountAsync(CancellationToken ct = default) =>
        _db.Motos.LongCountAsync(ct);
}