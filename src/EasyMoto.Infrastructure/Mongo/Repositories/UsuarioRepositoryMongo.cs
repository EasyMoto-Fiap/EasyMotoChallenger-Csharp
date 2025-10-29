using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using MongoDB.Driver;

namespace EasyMoto.Infrastructure.Mongo.Repositories;

public sealed class UsuarioRepositoryMongo : MongoRepositoryBase<Usuario>, IUsuarioRepository, IRepository<Usuario>
{
    public UsuarioRepositoryMongo(IMongoDatabase db, MongoIntIdGenerator ids) : base(db, ids, "usuarios") { }

    public async Task<bool> EmailExisteAsync(string email)
    {
        var c = await Collection.CountDocumentsAsync(Builders<Usuario>.Filter.Eq("Email", email));
        return c > 0;
    }

    public async Task<Usuario?> ObterPorEmailAsync(string email)
    {
        return await Collection.Find(Builders<Usuario>.Filter.Eq("Email", email)).FirstOrDefaultAsync();
    }

    public async Task<(IList<Usuario> Items, int TotalCount, int Page, int PageSize)> ListAsync(int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        var filter = Builders<Usuario>.Filter.Empty;
        var totalL = await Collection.CountDocumentsAsync(filter);
        var items = await Collection.Find(filter)
            .Sort(SortByIdAsc())
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
        return (items, (int)totalL, page, pageSize);
    }

    async Task IRepository<Usuario>.AddAsync(Usuario entity)
    {
        await EnsureIntIdAsync(entity, default);
        await Collection.InsertOneAsync(entity);
    }

    async Task IRepository<Usuario>.UpdateAsync(Usuario entity)
    {
        await Collection.ReplaceOneAsync(IdEq(entity.Id), entity, new ReplaceOptions { IsUpsert = false });
    }

    async Task IRepository<Usuario>.DeleteAsync(Usuario entity)
    {
        await Collection.DeleteOneAsync(IdEq(entity.Id));
    }

    async Task<Usuario?> IRepository<Usuario>.GetByIdAsync(int id)
    {
        return await Collection.Find(IdEq(id)).FirstOrDefaultAsync();
    }

    async Task<IReadOnlyList<Usuario>> IRepository<Usuario>.ListAsync(int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        return await Collection.Find(Builders<Usuario>.Filter.Empty)
            .Sort(SortByIdAsc())
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
    }

    async Task<int> IRepository<Usuario>.CountAsync()
    {
        var total = await Collection.CountDocumentsAsync(Builders<Usuario>.Filter.Empty);
        return (int)total;
    }

    IQueryable<Usuario> IRepository<Usuario>.Query()
    {
        return Collection.AsQueryable();
    }
}
