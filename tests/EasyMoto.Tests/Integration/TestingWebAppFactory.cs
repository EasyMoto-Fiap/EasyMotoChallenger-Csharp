using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Tests.TestDoubles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EasyMoto.Tests.Integration
{
    public class TestingWebAppFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            builder.ConfigureAppConfiguration((ctx, config) =>
            {
                var overrides = new Dictionary<string, string?>
                {
                    ["Auth:ApiKey"] = "test-key",
                    ["Auth:HeaderName"] = "X-Api-Key",
                    ["Mongo:ConnectionString"] = "mongodb://localhost:27017",
                    ["Mongo:Database"] = "easymoto_tests"
                };

                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
                config.AddJsonFile("appsettings.Test.json", optional: true, reloadOnChange: false);
                config.AddInMemoryCollection(overrides);
            });

            builder.ConfigureServices(services =>
            {
                services.RemoveAll<IMotoRepository>();
                services.RemoveAll<IFilialRepository>();

                var motoRepo = new InMemoryMotoRepository();
                var filialRepo = new InMemoryFilialRepository(new[]
                {
                    new Filial("Filial Teste", "01001-000", "São Paulo", "SP")
                });

                services.AddSingleton<IMotoRepository>(motoRepo);
                services.AddSingleton<IFilialRepository>(filialRepo);
            });
        }
    }
}