using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using EasyMoto.Infrastructure.Persistence;
using EasyMoto.Domain.Repositories;
using EasyMoto.Infrastructure.Persistence.Repositories;

namespace EasyMoto.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IFilialRepository, FilialRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IMotoRepository, MotoRepository>();
            services.AddScoped<ILegendaStatusRepository, LegendaStatusRepository>();
            services.AddScoped<INotificacaoRepository, NotificacaoRepository>();

            return services;
        }
    }
}