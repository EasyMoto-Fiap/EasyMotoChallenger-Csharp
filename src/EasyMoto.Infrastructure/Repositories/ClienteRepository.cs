using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace EasyMoto.Infrastructure.Repositories;

public sealed class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _db;

    public ClienteRepository(AppDbContext db) => _db = db;

    public async Task<Cliente?> GetByIdAsync(int id, CancellationToken ct) =>
        await _db.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.IdCliente == id, ct);

    public async Task AddAsync(Cliente cliente, CancellationToken ct)
    {
        await _db.Clientes.AddAsync(cliente, ct);
    }

    public Task UpdateAsync(Cliente cliente, CancellationToken ct)
    {
        _db.Clientes.Update(cliente);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        var entity = await _db.Clientes.FirstOrDefaultAsync(c => c.IdCliente == id, ct);
        if (entity is not null) _db.Clientes.Remove(entity);
    }
    public async Task<bool> ExistsByCpfAsync(string cpf, CancellationToken ct)
    {
        var count = await _db.Clientes.CountAsync(c => c.CpfCliente == cpf, ct);
        return count > 0;
    }

    public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
}
