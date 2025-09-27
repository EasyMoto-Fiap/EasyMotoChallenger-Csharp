using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories;

public sealed class ClienteLocacaoRepository : IClienteLocacaoRepository
{
    private readonly AppDbContext _db;

    public ClienteLocacaoRepository(AppDbContext db) => _db = db;

    public Task<int> CountAsync(CancellationToken ct = default) =>
        _db.Locacoes.AsNoTracking().CountAsync(ct);

    public async Task<List<ClienteLocacao>> ListAsync(int page, int size, CancellationToken ct = default) =>
        await _db.Locacoes
            .AsNoTracking()
            .OrderByDescending(x => x.Id)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(ct);

    public Task<ClienteLocacao?> GetByIdAsync(int id, CancellationToken ct = default) =>
        _db.Locacoes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task AddAsync(ClienteLocacao entity, CancellationToken ct = default)
    {
        await _db.Locacoes.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(ClienteLocacao entity, CancellationToken ct = default)
    {
        _db.Locacoes.Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(ClienteLocacao entity, CancellationToken ct = default)
    {
        _db.Locacoes.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}