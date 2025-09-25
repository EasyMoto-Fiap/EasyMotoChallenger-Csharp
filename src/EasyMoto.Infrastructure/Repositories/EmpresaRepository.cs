using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories;

public sealed class EmpresaRepository : IEmpresaRepository
{
    private readonly AppDbContext _db;
    public EmpresaRepository(AppDbContext db) => _db = db;

    public async Task<Empresa> AddAsync(Empresa entity, CancellationToken ct)
    {
        _db.Set<Empresa>().Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity;
    }

    public Task<long> CountAsync(CancellationToken ct) =>
        _db.Set<Empresa>().LongCountAsync(ct);

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var e = await _db.Set<Empresa>().FirstOrDefaultAsync(x => x.IdEmpresa == id, ct);
        if (e is null) return false;
        _db.Remove(e);
        await _db.SaveChangesAsync(ct);
        return true;
    }

    public Task<Empresa?> GetByIdAsync(int id, CancellationToken ct) =>
        _db.Set<Empresa>().AsNoTracking().FirstOrDefaultAsync(x => x.IdEmpresa == id, ct);

    public async Task<IReadOnlyList<Empresa>> ListAsync(int page, int size, CancellationToken ct)
    {
        if (page < 1) page = 1;
        if (size < 1) size = 10;
        return await _db.Set<Empresa>()
            .AsNoTracking()
            .OrderBy(x => x.NomeEmpresa)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(ct);
    }

    public async Task<Empresa?> UpdateAsync(int id, Empresa input, CancellationToken ct)
    {
        var e = await _db.Set<Empresa>().FirstOrDefaultAsync(x => x.IdEmpresa == id, ct);
        if (e is null) return null;
        e.Update(input.NomeEmpresa, input.Cnpj);
        await _db.SaveChangesAsync(ct);
        return e;
    }
}