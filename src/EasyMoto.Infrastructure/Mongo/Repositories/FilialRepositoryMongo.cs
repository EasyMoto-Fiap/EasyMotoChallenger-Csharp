using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using MongoDB.Driver;

namespace EasyMoto.Infrastructure.Mongo.Repositories;

public sealed class FilialRepositoryMongo : MongoRepositoryBase<Filial>, IFilialRepository, IRepository<Filial>
{
    public FilialRepositoryMongo(IMongoDatabase db, MongoIntIdGenerator ids) : base(db, ids, "filiais") { }

    async Task IRepository<Filial>.AddAsync(Filial entity)
    {
        await EnsureIntIdAsync(entity, default);
        await Collection.InsertOneAsync(entity);
    }

    async Task IRepository<Filial>.UpdateAsync(Filial entity)
    {
        await Collection.ReplaceOneAsync(IdEq(entity.Id), entity, new ReplaceOptions { IsUpsert = false });
    }

    async Task IRepository<Filial>.DeleteAsync(Filial entity)
    {
        await Collection.DeleteOneAsync(IdEq(entity.Id));
    }

    async Task<Filial?> IRepository<Filial>.GetByIdAsync(int id)
    {
        return await Collection.Find(IdEq(id)).FirstOrDefaultAsync();
    }

    async Task<IReadOnlyList<Filial>> IRepository<Filial>.ListAsync(int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        return await Collection.Find(Builders<Filial>.Filter.Empty)
            .Sort(SortByIdAsc())
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
    }

    async Task<int> IRepository<Filial>.CountAsync()
    {
        var total = await Collection.CountDocumentsAsync(Builders<Filial>.Filter.Empty);
        return (int)total;
    }

    IQueryable<Filial> IRepository<Filial>.Query()
    {
        return Collection.AsQueryable();
    }
}