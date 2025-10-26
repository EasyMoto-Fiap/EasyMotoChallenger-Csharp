using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using MongoDB.Driver;

namespace EasyMoto.Infrastructure.Mongo.Repositories;

public sealed class MotoRepositoryMongo : MongoRepositoryBase<Moto>, IMotoRepository, IRepository<Moto>
{
    public MotoRepositoryMongo(IMongoDatabase db, MongoIntIdGenerator ids) : base(db, ids, "motos") { }

    async Task IRepository<Moto>.AddAsync(Moto entity)
    {
        await EnsureIntIdAsync(entity, default);
        await Collection.InsertOneAsync(entity);
    }

    async Task IRepository<Moto>.UpdateAsync(Moto entity)
    {
        await Collection.ReplaceOneAsync(IdEq(entity.Id), entity, new ReplaceOptions { IsUpsert = false });
    }

    async Task IRepository<Moto>.DeleteAsync(Moto entity)
    {
        await Collection.DeleteOneAsync(IdEq(entity.Id));
    }

    async Task<Moto?> IRepository<Moto>.GetByIdAsync(int id)
    {
        return await Collection.Find(IdEq(id)).FirstOrDefaultAsync();
    }

    async Task<IReadOnlyList<Moto>> IRepository<Moto>.ListAsync(int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        return await Collection.Find(Builders<Moto>.Filter.Empty)
            .Sort(SortByIdAsc())
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
    }

    async Task<int> IRepository<Moto>.CountAsync()
    {
        var total = await Collection.CountDocumentsAsync(Builders<Moto>.Filter.Empty);
        return (int)total;
    }

    IQueryable<Moto> IRepository<Moto>.Query()
    {
        return Collection.AsQueryable();
    }
}