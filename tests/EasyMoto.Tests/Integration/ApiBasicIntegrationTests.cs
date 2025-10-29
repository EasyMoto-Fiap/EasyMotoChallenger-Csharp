using System.Net.Http.Json;

namespace EasyMoto.Tests.Integration
{
    public class ApiBasicIntegrationTests : IClassFixture<TestingWebAppFactory>
    {
        private readonly HttpClient _client;

        public ApiBasicIntegrationTests(TestingWebAppFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Health_DeveRetornarOk()
        {
            var resp = await _client.GetAsync("/health");
            var body = await resp.Content.ReadAsStringAsync();
            Assert.True(resp.IsSuccessStatusCode, body);
        }

        [Fact]
        public async Task Motos_Post_ComApiKeyValida_DeveRetornar2xx()
        {
            using var req = new HttpRequestMessage(HttpMethod.Post, "/api/motos");
            req.Headers.Add("X-Api-Key", "test-key");
            req.Content = JsonContent.Create(new
            {
                modelo = "Biz 125",
                ano = 2023,
                placa = "XYZ1A23",
                cor = "Vermelha",
                ativo = true,
                filialId = 1
            });

            var resp = await _client.SendAsync(req);
            var body = await resp.Content.ReadAsStringAsync();
            Assert.True(resp.IsSuccessStatusCode, body);
        }
    }
}