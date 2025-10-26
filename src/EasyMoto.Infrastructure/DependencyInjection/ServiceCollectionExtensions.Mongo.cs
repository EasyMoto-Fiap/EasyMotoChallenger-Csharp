using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Mongo.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace EasyMoto.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensionsMongo
{
    public static IServiceCollection AddInfrastructureMongo(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<MongoIntIdGenerator>();
        services.AddScoped<IFilialRepository, FilialRepositoryMongo>();
        services.AddScoped<IUsuarioRepository, UsuarioRepositoryMongo>();
        services.AddScoped<IMotoRepository, MotoRepositoryMongo>();
        services.AddScoped<ILegendaStatusRepository, LegendaStatusRepositoryMongo>();
        services.AddScoped<INotificacaoRepository, NotificacaoRepositoryMongo>();
        return services;
    }
}