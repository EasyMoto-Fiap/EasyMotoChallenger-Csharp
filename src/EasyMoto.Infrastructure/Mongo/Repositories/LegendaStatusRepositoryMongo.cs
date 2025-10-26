using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using MongoDB.Driver;

namespace EasyMoto.Infrastructure.Mongo.Repositories;

public sealed class LegendaStatusRepositoryMongo : MongoRepositoryBase<LegendaStatus>, ILegendaStatusRepository, IRepository<LegendaStatus>
{
    public LegendaStatusRepositoryMongo(IMongoDatabase db, MongoIntIdGenerator ids) : base(db, ids, "legendasstatus") { }

    async Task IRepository<LegendaStatus>.AddAsync(LegendaStatus entity)
    {
        await EnsureIntIdAsync(entity, default);
        await Collection.InsertOneAsync(entity);
    }

    async Task IRepository<LegendaStatus>.UpdateAsync(LegendaStatus entity)
    {
        await Collection.ReplaceOneAsync(IdEq(entity.Id), entity, new ReplaceOptions { IsUpsert = false });
    }

    async Task IRepository<LegendaStatus>.DeleteAsync(LegendaStatus entity)
    {
        await Collection.DeleteOneAsync(IdEq(entity.Id));
    }

    async Task<LegendaStatus?> IRepository<LegendaStatus>.GetByIdAsync(int id)
    {
        return await Collection.Find(IdEq(id)).FirstOrDefaultAsync();
    }

    async Task<IReadOnlyList<LegendaStatus>> IRepository<LegendaStatus>.ListAsync(int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        return await Collection.Find(Builders<LegendaStatus>.Filter.Empty)
            .Sort(SortByIdAsc())
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
    }

    async Task<int> IRepository<LegendaStatus>.CountAsync()
    {
        var total = await Collection.CountDocumentsAsync(Builders<LegendaStatus>.Filter.Empty);
        return (int)total;
    }

    IQueryable<LegendaStatus> IRepository<LegendaStatus>.Query()
    {
        return Collection.AsQueryable();
    }
}