using System.Linq;
using System.Reflection;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Mongo;
using EasyMoto.Infrastructure.Mongo.Documents;
using MongoDB.Driver;

namespace EasyMoto.Infrastructure.Repositories;

public sealed class LocacaoRepositoryMongo : ILocacaoRepository
{
    private readonly IMongoCollection<LocacaoDocument> _collection;

    public LocacaoRepositoryMongo(MongoDbContext ctx)
    {
        _collection = ctx.Database.GetCollection<LocacaoDocument>("locacoes");
    }

    public async Task<Locacao?> GetByIdAsync(int id, CancellationToken ct)
    {
        var doc = await _collection.Find(d => d.IdLocacao == id).FirstOrDefaultAsync(ct);
        return doc is null ? null : ToEntity(doc);
    }

    public async Task<List<Locacao>> GetAllAsync(CancellationToken ct)
    {
        var docs = await _collection.Find(Builders<LocacaoDocument>.Filter.Empty).ToListAsync(ct);
        return docs.Select(ToEntity).ToList();
    }

    public async Task AddAsync(Locacao locacao, CancellationToken ct)
    {
        var lastId = await _collection.Find(Builders<LocacaoDocument>.Filter.Empty)
            .SortByDescending(d => d.IdLocacao)
            .Project(d => d.IdLocacao)
            .FirstOrDefaultAsync(ct);

        var nextId = lastId == 0 ? 1 : lastId + 1;

        var doc = new LocacaoDocument
        {
            IdLocacao = nextId,
            ClienteId = locacao.ClienteId,
            DataInicio = locacao.Periodo.Inicio,
            DataFim = locacao.Periodo.Fim,
            StatusLocacao = locacao.StatusLocacao
        };

        await _collection.InsertOneAsync(doc, cancellationToken: ct);
        SetId(locacao, nextId);
    }

    public async Task UpdateAsync(Locacao locacao, CancellationToken ct)
    {
        var update = Builders<LocacaoDocument>.Update
            .Set(d => d.ClienteId, locacao.ClienteId)
            .Set(d => d.DataInicio, locacao.Periodo.Inicio)
            .Set(d => d.DataFim, locacao.Periodo.Fim)
            .Set(d => d.StatusLocacao, locacao.StatusLocacao);

        await _collection.UpdateOneAsync(d => d.IdLocacao == locacao.IdLocacao, update, cancellationToken: ct);
    }

    public Task DeleteAsync(int id, CancellationToken ct)
    {
        return _collection.DeleteOneAsync(d => d.IdLocacao == id, ct);
    }

    public Task SaveChangesAsync(CancellationToken ct) => Task.CompletedTask;

    private static Locacao ToEntity(LocacaoDocument d)
    {
        var fim = d.DataFim ?? d.DataInicio;
        var e = new Locacao(d.ClienteId, d.DataInicio, fim, d.StatusLocacao);
        SetId(e, d.IdLocacao);
        return e;
    }

    private static void SetId(Locacao entity, int id)
    {
        var p = typeof(Locacao).GetProperty(nameof(Locacao.IdLocacao), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        p?.SetValue(entity, id);
    }
}
