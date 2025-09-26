using Microsoft.OpenApi.Models;
using Microsoft.OpenApi;

namespace EasyMoto.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EasyMoto API", Version = "v1" });
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger(c => { c.OpenApiVersion = OpenApiSpecVersion.OpenApi2_0; });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EasyMoto API v1");
                c.RoutePrefix = "swagger";
            });
            return app;
        }
    }
}