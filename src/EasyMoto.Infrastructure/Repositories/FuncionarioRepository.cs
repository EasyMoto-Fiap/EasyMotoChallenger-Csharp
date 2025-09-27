using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories;

public sealed class FuncionarioRepository : IFuncionarioRepository
{
    private readonly AppDbContext _db;

    public FuncionarioRepository(AppDbContext db) => _db = db;

    public Task<Funcionario?> GetByIdAsync(int id, CancellationToken ct = default)
        => _db.Set<Funcionario>().FirstOrDefaultAsync(x => x.Id == id, ct);

    public Task<int> CountAsync(CancellationToken ct = default)
        => _db.Set<Funcionario>().CountAsync(ct);

    public async Task<List<Funcionario>> ListAsync(int page, int size, CancellationToken ct = default)
    {
        var skip = (page <= 0 ? 0 : page - 1) * (size <= 0 ? 10 : size);
        var take = size <= 0 ? 10 : size;
        return await _db.Set<Funcionario>()
            .OrderBy(x => x.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Funcionario entity, CancellationToken ct = default)
    {
        await _db.Set<Funcionario>().AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Funcionario entity, CancellationToken ct = default)
    {
        _db.Set<Funcionario>().Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Funcionario entity, CancellationToken ct = default)
    {
        _db.Set<Funcionario>().Remove(entity);
        await _db.SaveChangesAsync(ct);
    }
}