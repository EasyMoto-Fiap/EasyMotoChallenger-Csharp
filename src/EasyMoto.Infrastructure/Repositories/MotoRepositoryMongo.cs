using System.Reflection;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Mongo;
using EasyMoto.Infrastructure.Mongo.Documents;
using MongoDB.Driver;

namespace EasyMoto.Infrastructure.Repositories;

public class MotoRepositoryMongo : IMotoRepository
{
    private readonly IMongoCollection<MotoDocument> _collection;

    public MotoRepositoryMongo(MongoDbContext ctx)
    {
        _collection = ctx.Database.GetCollection<MotoDocument>("motos");
    }

    public async Task<List<Moto>> GetAllAsync(CancellationToken ct)
    {
        var docs = await _collection.Find(FilterDefinition<MotoDocument>.Empty).SortBy(d => d.IdMoto).ToListAsync(ct);
        return docs.Select(ToEntity).ToList();
    }

    public async Task<Moto?> GetByIdAsync(int id, CancellationToken ct)
    {
        var doc = await _collection.Find(d => d.IdMoto == id).FirstOrDefaultAsync(ct);
        return doc is null ? null : ToEntity(doc);
    }

    public async Task AddAsync(Moto moto, CancellationToken ct)
    {
        var last = await _collection.Find(FilterDefinition<MotoDocument>.Empty)
            .SortByDescending(d => d.IdMoto).Limit(1).FirstOrDefaultAsync(ct);
        var nextId = (last?.IdMoto ?? 0) + 1;

        var doc = new MotoDocument
        {
            IdMoto = nextId,
            ModeloMoto = GetProp<string>(moto, "ModeloMoto") ?? GetProp<string>(moto, "Modelo") ?? "",
            PlacaMoto = GetProp<string>(moto, "PlacaMoto") ?? GetProp<string>(moto, "Placa") ?? "",
            AnoMoto = GetProp<int>(moto, "AnoMoto") != 0 ? GetProp<int>(moto, "AnoMoto") : GetProp<int>(moto, "Ano"),
            StatusMoto = GetStatusInt(moto)
        };

        await _collection.InsertOneAsync(doc, cancellationToken: ct);
        SetId(moto, nextId);
    }

    public async Task UpdateAsync(Moto moto, CancellationToken ct)
    {
        var update = Builders<MotoDocument>.Update
            .Set(d => d.ModeloMoto, GetProp<string>(moto, "ModeloMoto") ?? GetProp<string>(moto, "Modelo") ?? "")
            .Set(d => d.PlacaMoto, GetProp<string>(moto, "PlacaMoto") ?? GetProp<string>(moto, "Placa") ?? "")
            .Set(d => d.AnoMoto, GetProp<int>(moto, "AnoMoto") != 0 ? GetProp<int>(moto, "AnoMoto") : GetProp<int>(moto, "Ano"))
            .Set(d => d.StatusMoto, GetStatusInt(moto));

        await _collection.UpdateOneAsync(d => d.IdMoto == GetId(moto), update, cancellationToken: ct);
    }

    public Task DeleteAsync(int id, CancellationToken ct) =>
        _collection.DeleteOneAsync(d => d.IdMoto == id, ct);

    public async Task<bool> ExistsByPlacaAsync(string placa, CancellationToken ct)
    {
        var count = await _collection.CountDocumentsAsync(d => d.PlacaMoto == placa, cancellationToken: ct);
        return count > 0;
    }

    public Task SaveChangesAsync(CancellationToken ct) => Task.CompletedTask;

    private static Moto ToEntity(MotoDocument d)
    {
        var m = CreateMoto(d.ModeloMoto, d.PlacaMoto, d.AnoMoto);
        SetId(m, d.IdMoto);
        TrySetStatus(m, d.StatusMoto);
        return m;
    }

    private static Moto CreateMoto(string modelo, string placa, int ano)
    {
        var ctor = typeof(Moto).GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .OrderByDescending(c => c.GetParameters().Length)
            .First();
        var ps = ctor.GetParameters();
        var args = ps.Select(p =>
        {
            if (p.Name!.Equals("modelo", StringComparison.OrdinalIgnoreCase) || p.Name!.Equals("modeloMoto", StringComparison.OrdinalIgnoreCase)) return (object)modelo;
            if (p.Name!.Equals("placa", StringComparison.OrdinalIgnoreCase) || p.Name!.Equals("placaMoto", StringComparison.OrdinalIgnoreCase)) return (object)placa;
            if (p.Name!.Equals("ano", StringComparison.OrdinalIgnoreCase) || p.Name!.Equals("anoMoto", StringComparison.OrdinalIgnoreCase)) return (object)ano;
            return p.ParameterType.IsValueType ? Activator.CreateInstance(p.ParameterType)! : null!;
        }).ToArray();
        return (Moto)Activator.CreateInstance(typeof(Moto), args)!;
    }

    private static void SetId(Moto entity, int id)
    {
        var p = typeof(Moto).GetProperty("IdMoto", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        p?.SetValue(entity, id);
    }

    private static int GetId(Moto entity)
    {
        var p = typeof(Moto).GetProperty("IdMoto", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var v = p?.GetValue(entity);
        return v is null ? 0 : Convert.ToInt32(v);
    }

    private static T? GetProp<T>(Moto entity, string name)
    {
        var p = typeof(Moto).GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var v = p?.GetValue(entity);
        if (v is null) return default;
        return (T)Convert.ChangeType(v, typeof(T));
    }

    private static int GetStatusInt(Moto entity)
    {
        var p = typeof(Moto).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .FirstOrDefault(x => x.Name.Equals("StatusMoto", StringComparison.OrdinalIgnoreCase) || x.Name.Equals("Status", StringComparison.OrdinalIgnoreCase));
        var v = p?.GetValue(entity);
        return v is null ? 0 : Convert.ToInt32(v);
    }

    private static void TrySetStatus(Moto entity, int status)
    {
        var p = typeof(Moto).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .FirstOrDefault(x => x.Name.Equals("StatusMoto", StringComparison.OrdinalIgnoreCase) || x.Name.Equals("Status", StringComparison.OrdinalIgnoreCase));
        p?.SetValue(entity, Enum.ToObject(p.PropertyType.IsEnum ? p.PropertyType : typeof(int), status));
    }
}
