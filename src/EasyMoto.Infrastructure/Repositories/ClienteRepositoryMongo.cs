using System.Reflection;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Domain.ValueObjects;
using EasyMoto.Infrastructure.Mongo;
using EasyMoto.Infrastructure.Mongo.Documents;
using MongoDB.Driver;

namespace EasyMoto.Infrastructure.Repositories;

public class ClienteRepositoryMongo : IClienteRepository
{
    private readonly IMongoCollection<ClienteDocument> _collection;

    public ClienteRepositoryMongo(MongoDbContext ctx)
    {
        _collection = ctx.Database.GetCollection<ClienteDocument>("clientes");
    }

    public async Task<List<Cliente>> GetAllAsync(CancellationToken ct)
    {
        var docs = await _collection.Find(FilterDefinition<ClienteDocument>.Empty).SortBy(d => d.IdCliente).ToListAsync(ct);
        return docs.Select(ToEntity).ToList();
    }

    public async Task<Cliente?> GetByIdAsync(int id, CancellationToken ct)
    {
        var doc = await _collection.Find(d => d.IdCliente == id).FirstOrDefaultAsync(ct);
        return doc is null ? null : ToEntity(doc);
    }

    public async Task AddAsync(Cliente cliente, CancellationToken ct)
    {
        var last = await _collection.Find(FilterDefinition<ClienteDocument>.Empty)
            .SortByDescending(d => d.IdCliente).Limit(1).FirstOrDefaultAsync(ct);
        var nextId = (last?.IdCliente ?? 0) + 1;

        var doc = new ClienteDocument
        {
            IdCliente = nextId,
            NomeCliente = cliente.NomeCliente,
            CpfCliente = cliente.CpfCliente.Value,
            TelefoneCliente = cliente.TelefoneCliente,
            EmailCliente = cliente.EmailCliente
        };

        await _collection.InsertOneAsync(doc, cancellationToken: ct);
        SetId(cliente, nextId);
    }

    public async Task UpdateAsync(Cliente cliente, CancellationToken ct)
    {
        var update = Builders<ClienteDocument>.Update
            .Set(d => d.NomeCliente, cliente.NomeCliente)
            .Set(d => d.CpfCliente, cliente.CpfCliente.Value)
            .Set(d => d.TelefoneCliente, cliente.TelefoneCliente)
            .Set(d => d.EmailCliente, cliente.EmailCliente);

        await _collection.UpdateOneAsync(d => d.IdCliente == cliente.IdCliente, update, cancellationToken: ct);
    }

    public Task DeleteAsync(int id, CancellationToken ct) =>
        _collection.DeleteOneAsync(d => d.IdCliente == id, ct);

    public async Task<bool> ExistsByCpfAsync(string cpf, CancellationToken ct)
    {
        var digits = Cpf.From(cpf).Value;
        var count = await _collection.CountDocumentsAsync(d => d.CpfCliente == digits, cancellationToken: ct);
        return count > 0;
    }

    public Task SaveChangesAsync(CancellationToken ct) => Task.CompletedTask;

    private static Cliente ToEntity(ClienteDocument d)
    {
        var e = new Cliente(d.NomeCliente, d.CpfCliente, d.TelefoneCliente, d.EmailCliente);
        SetId(e, d.IdCliente);
        return e;
    }

    private static void SetId(Cliente entity, int id)
    {
        var p = typeof(Cliente).GetProperty(nameof(Cliente.IdCliente), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        p?.SetValue(entity, id);
    }
}
