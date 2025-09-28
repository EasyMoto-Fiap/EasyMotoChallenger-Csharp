using EasyMoto.Application.UseCases.Filiais;
using Microsoft.Extensions.DependencyInjection;

namespace EasyMoto.Application.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IListFiliaisUseCase, ListFiliaisUseCase>();
            services.AddScoped<IGetFilialUseCase, GetFilialUseCase>();
            services.AddScoped<ICreateFilialUseCase, CreateFilialUseCase>();
            services.AddScoped<IUpdateFilialUseCase, UpdateFilialUseCase>();
            services.AddScoped<IDeleteFilialUseCase, DeleteFilialUseCase>();
            return services;
        }
    }
}