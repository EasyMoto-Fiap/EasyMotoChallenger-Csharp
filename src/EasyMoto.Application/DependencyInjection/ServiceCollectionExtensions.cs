using EasyMoto.Application.UseCases.Filiais;
using EasyMoto.Application.UseCases.Usuarios.Implementations;
using EasyMoto.Application.UseCases.Usuarios.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EasyMoto.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateFilialUseCase, CreateFilialUseCase>();
        services.AddScoped<IGetFilialUseCase, GetFilialUseCase>();
        services.AddScoped<IListFiliaisUseCase, ListFiliaisUseCase>();
        services.AddScoped<IUpdateFilialUseCase, UpdateFilialUseCase>();
        services.AddScoped<IDeleteFilialUseCase, DeleteFilialUseCase>();

        services.AddScoped<ICreateUsuarioUseCase, CreateUsuarioUseCase>();
        services.AddScoped<IGetUsuarioUseCase, GetUsuarioUseCase>();
        services.AddScoped<IListUsuariosUseCase, ListUsuariosUseCase>();
        services.AddScoped<IUpdateUsuarioUseCase, UpdateUsuarioUseCase>();
        services.AddScoped<IDeleteUsuarioUseCase, DeleteUsuarioUseCase>();

        return services;
    }
}