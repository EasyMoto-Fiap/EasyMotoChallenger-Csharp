using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories;

public sealed class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _db;

    public ClienteRepository(AppDbContext db) => _db = db;

    public Task<int> CountAsync(CancellationToken ct) =>
        _db.Clientes.AsNoTracking().CountAsync(ct);

    public async Task<IReadOnlyList<Cliente>> ListAsync(int page, int size, CancellationToken ct)
    {
        return await _db.Clientes.AsNoTracking()
            .OrderBy(c => c.NomeCliente)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(ct);
    }

    public Task<Cliente?> GetByIdAsync(int id, CancellationToken ct) =>
        _db.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, ct);

    public async Task AddAsync(Cliente entity, CancellationToken ct)
    {
        await _db.Clientes.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Cliente entity, CancellationToken ct)
    {
        _db.Clientes.Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        await _db.Clientes.Where(c => c.Id == id).ExecuteDeleteAsync(ct);
    }

    public Task<bool> ExistsCpfAsync(string cpf, int? ignoreId, CancellationToken ct) =>
        _db.Clientes.AnyAsync(c => c.CpfCliente == cpf && (ignoreId == null || c.Id != ignoreId), ct);
}