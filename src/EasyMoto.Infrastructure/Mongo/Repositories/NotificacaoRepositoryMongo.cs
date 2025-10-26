using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using MongoDB.Driver;

namespace EasyMoto.Infrastructure.Mongo.Repositories;

public sealed class NotificacaoRepositoryMongo : MongoRepositoryBase<Notificacao>, INotificacaoRepository, IRepository<Notificacao>
{
    private readonly IMongoCollection<Notificacao> _col;

    public NotificacaoRepositoryMongo(IMongoDatabase db, MongoIntIdGenerator ids) : base(db, ids, "notificacoes")
    {
        _col = base.Collection;
    }

    async Task IRepository<Notificacao>.AddAsync(Notificacao entity)
    {
        await EnsureIntIdAsync(entity, default);
        await _col.InsertOneAsync(entity);
    }

    async Task IRepository<Notificacao>.UpdateAsync(Notificacao entity)
    {
        await _col.ReplaceOneAsync(IdEq(entity.Id), entity, new ReplaceOptions { IsUpsert = false });
    }

    async Task IRepository<Notificacao>.DeleteAsync(Notificacao entity)
    {
        await _col.DeleteOneAsync(IdEq(entity.Id));
    }

    async Task<Notificacao?> IRepository<Notificacao>.GetByIdAsync(int id)
    {
        return await _col.Find(IdEq(id)).FirstOrDefaultAsync();
    }

    async Task<IReadOnlyList<Notificacao>> IRepository<Notificacao>.ListAsync(int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        return await _col.Find(Builders<Notificacao>.Filter.Empty)
            .Sort(SortByIdAsc())
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
    }

    async Task<int> IRepository<Notificacao>.CountAsync()
    {
        var total = await _col.CountDocumentsAsync(Builders<Notificacao>.Filter.Empty);
        return (int)total;
    }

    IQueryable<Notificacao> IRepository<Notificacao>.Query()
    {
        return _col.AsQueryable();
    }

    public async Task MarcarComoLidaAsync(int notificacaoId, int usuarioId)
    {
        var filter = IdEq(notificacaoId);
        var leitura = new NotificacaoLeitura { NotificacaoId = notificacaoId, UsuarioId = usuarioId, LidoEm = DateTime.UtcNow };
        var update = Builders<Notificacao>.Update.AddToSet(n => n.Leituras, leitura);
        await _col.UpdateOneAsync(filter, update);
    }
}
