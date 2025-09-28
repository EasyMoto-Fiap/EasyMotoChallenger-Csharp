using EasyMoto.Application.UseCases.Filiais;
using EasyMoto.Application.UseCases.Usuarios.Implementations;
using EasyMoto.Application.UseCases.Usuarios.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using EasyMoto.Application.UseCases.Motos.Implementations;
using EasyMoto.Application.UseCases.Motos.Interfaces;

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
        
        services.AddScoped<ICreateMotoUseCase, CreateMotoUseCase>();
        services.AddScoped<IGetMotoUseCase, GetMotoUseCase>();
        services.AddScoped<IListMotosUseCase, ListMotosUseCase>();
        services.AddScoped<IUpdateMotoUseCase, UpdateMotoUseCase>();
        services.AddScoped<IDeleteMotoUseCase, DeleteMotoUseCase>();

        return services;
    }
}