using System.Linq.Expressions;
using System.Reflection;
using MongoDB.Driver;

namespace EasyMoto.Infrastructure.Mongo.Repositories;

public abstract class MongoRepositoryBase<TEntity> where TEntity : class
{
    protected readonly IMongoCollection<TEntity> Collection;
    private readonly MongoIntIdGenerator _ids;
    private static readonly PropertyInfo? IdProp = typeof(TEntity).GetProperty("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

    protected MongoRepositoryBase(IMongoDatabase db, MongoIntIdGenerator ids, string? collectionName = null)
    {
        Collection = db.GetCollection<TEntity>(collectionName ?? (typeof(TEntity).Name.ToLowerInvariant() + "s"));
        _ids = ids;
    }

    protected async Task EnsureIntIdAsync(TEntity entity, CancellationToken ct)
    {
        if (IdProp == null) return;
        var current = IdProp.GetValue(entity);
        if (current is int i && i > 0) return;
        var next = await _ids.NextForAsync<TEntity>(ct);
        IdProp.SetValue(entity, next);
    }

    protected static FilterDefinition<TEntity> IdEq(int id) => Builders<TEntity>.Filter.Eq("Id", id);

    protected static SortDefinition<TEntity> SortByIdAsc() => Builders<TEntity>.Sort.Ascending("Id");

    protected static Expression<Func<TEntity, bool>> Eq<TValue>(string field, TValue value)
    {
        var p = Expression.Parameter(typeof(TEntity), "x");
        var prop = Expression.PropertyOrField(p, field);
        var val = Expression.Constant(value, typeof(TValue));
        var body = Expression.Equal(prop, val);
        return Expression.Lambda<Func<TEntity, bool>>(body, p);
    }
}
