using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories;

public sealed class LocacaoRepository : ILocacaoRepository
{
    private readonly AppDbContext _db;

    public LocacaoRepository(AppDbContext db) => _db = db;

    public Task<Locacao?> GetByIdAsync(int id, CancellationToken ct) =>
        _db.Locacoes.FirstOrDefaultAsync(l => l.IdLocacao == id, ct);

    public Task<List<Locacao>> GetAllAsync(CancellationToken ct) =>
        _db.Locacoes.AsNoTracking().OrderBy(l => l.IdLocacao).ToListAsync(ct);

    public async Task AddAsync(Locacao locacao, CancellationToken ct)
    {
        await _db.Locacoes.AddAsync(locacao, ct);
    }

    public Task UpdateAsync(Locacao locacao, CancellationToken ct)
    {
        _db.Locacoes.Update(locacao);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        var entity = await _db.Locacoes.FirstOrDefaultAsync(l => l.IdLocacao == id, ct);
        if (entity is not null) _db.Locacoes.Remove(entity);
    }

    public async Task<bool> ExisteSobreposicaoAsync(int motoId, DateTime inicio, DateTime fim, CancellationToken ct)
    {
        var count = await _db.Locacoes.CountAsync(
            l => l.MotoId == motoId
                 && l.Status == LocacaoStatus.Aberta
                 && l.Periodo.Inicio <= fim
                 && inicio <= l.Periodo.Fim,
            ct);
        return count > 0;
    }

    public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
}
