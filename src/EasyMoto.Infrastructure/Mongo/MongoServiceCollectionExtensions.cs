using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace EasyMoto.Infrastructure.Mongo;

public static class MongoServiceCollectionExtensions
{
    public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection("Mongo").Get<MongoOptions>() ?? throw new InvalidOperationException("Mongo options missing");
        var pack = new ConventionPack
        {
            new CamelCaseElementNameConvention(),
            new IgnoreExtraElementsConvention(true),
            new EnumRepresentationConvention(BsonType.String)
        };
        ConventionRegistry.Register("easymoto-mongo", pack, _ => true);
        services.AddSingleton<IMongoClient>(_ => new MongoClient(options.ConnectionString));
        services.AddSingleton(sp => sp.GetRequiredService<IMongoClient>().GetDatabase(options.Database));
        return services;
    }
}