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

public sealed class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _db;

    public ClienteRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Cliente entity, CancellationToken ct = default)
    {
        await _db.Clientes.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Cliente entity, CancellationToken ct = default)
    {
        _db.Clientes.Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Cliente entity, CancellationToken ct = default)
    {
        _db.Clientes.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }

    public Task<Cliente?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        _db.Clientes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<List<Cliente>> ListAsync(int page, int size, CancellationToken ct = default) =>
        await _db.Clientes.AsNoTracking()
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(ct);

    public Task<long> CountAsync(CancellationToken ct = default) =>
        _db.Clientes.LongCountAsync(ct);
}