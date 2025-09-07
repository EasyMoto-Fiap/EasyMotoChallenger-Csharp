using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Domain.ValueObjects;
using EasyMoto.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Infrastructure.Repositories;

public sealed class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _db;

    public ClienteRepository(AppDbContext db) => _db = db;

    public Task<List<Cliente>> GetAllAsync(CancellationToken ct) =>
        _db.Clientes.AsNoTracking().OrderBy(c => c.IdCliente).ToListAsync(ct);

    public Task<Cliente?> GetByIdAsync(int id, CancellationToken ct) =>
        _db.Clientes.FirstOrDefaultAsync(c => c.IdCliente == id, ct);

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
        var cpfVo = Cpf.From(cpf);
        var count = await _db.Clientes.CountAsync(c => c.CpfCliente == cpfVo, ct);
        return count > 0;
    }

    public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
}
