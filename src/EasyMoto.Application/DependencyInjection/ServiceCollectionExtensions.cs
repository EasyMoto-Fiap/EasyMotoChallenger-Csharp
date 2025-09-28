using EasyMoto.Application.UseCases.Filiais;
using EasyMoto.Application.UseCases.Usuarios.Implementations;
using EasyMoto.Application.UseCases.Usuarios.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using EasyMoto.Application.UseCases.Motos.Implementations;
using EasyMoto.Application.UseCases.Motos.Interfaces;
using EasyMoto.Application.UseCases.Legendas.Interfaces;
using EasyMoto.Application.UseCases.Legendas.Implementations;
using EasyMoto.Application.UseCases.Notificacoes.Interfaces;
using EasyMoto.Application.UseCases.Notificacoes.Implementations;

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

        services.AddScoped<ICreateLegendaStatusUseCase, CreateLegendaStatusUseCase>();
        services.AddScoped<IGetLegendaStatusUseCase, GetLegendaStatusUseCase>();
        services.AddScoped<IListLegendasStatusUseCase, ListLegendasStatusUseCase>();
        services.AddScoped<IUpdateLegendaStatusUseCase, UpdateLegendaStatusUseCase>();
        services.AddScoped<IDeleteLegendaStatusUseCase, DeleteLegendaStatusUseCase>();
        
        services.AddScoped<ICreateNotificacaoUseCase, CreateNotificacaoUseCase>();
        services.AddScoped<IGetNotificacaoUseCase, GetNotificacaoUseCase>();
        services.AddScoped<IListNotificacoesUseCase, ListNotificacoesUseCase>();
        services.AddScoped<IMarkNotificacaoLidaUseCase, MarkNotificacaoLidaUseCase>();
        services.AddScoped<IDeleteNotificacaoUseCase, DeleteNotificacaoUseCase>();
        return services;
    }
}