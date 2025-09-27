using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories;

public sealed class LocalizacaoRepository : ILocalizacaoRepository
{
    private readonly AppDbContext _db;

    public LocalizacaoRepository(AppDbContext db) => _db = db;

    public Task<int> CountAsync(CancellationToken ct) =>
        _db.Localizacoes.AsNoTracking().CountAsync(ct);

    public async Task<IReadOnlyList<Localizacao>> ListAsync(int page, int size, CancellationToken ct)
    {
        var skip = page <= 1 ? 0 : (page - 1) * size;

        return await _db.Localizacoes
            .AsNoTracking()
            .OrderBy(l => l.Id)
            .Skip(skip)
            .Take(size)
            .ToListAsync(ct);
    }

    public Task<Localizacao?> GetByIdAsync(int id, CancellationToken ct) =>
        _db.Localizacoes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task AddAsync(Localizacao entity, CancellationToken ct)
    {
        _db.Localizacoes.Add(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Localizacao entity, CancellationToken ct)
    {
        _db.Localizacoes.Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        await _db.Localizacoes.Where(x => x.Id == id).ExecuteDeleteAsync(ct);
    }
}