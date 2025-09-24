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

public sealed class LocacaoRepository : ILocacaoRepository
{
    private readonly AppDbContext _db;

    public LocacaoRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Locacao entity, CancellationToken ct = default)
    {
        await _db.Locacoes.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Locacao entity, CancellationToken ct = default)
    {
        _db.Locacoes.Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Locacao entity, CancellationToken ct = default)
    {
        _db.Locacoes.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }

    public Task<Locacao?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        _db.Locacoes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<List<Locacao>> ListAsync(int page, int size, CancellationToken ct = default) =>
        await _db.Locacoes.AsNoTracking()
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(ct);

    public Task<long> CountAsync(CancellationToken ct = default) =>
        _db.Locacoes.LongCountAsync(ct);
}