using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Application.Shared.Hateoas;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Clientes
{
    public class ResourceClienteResponseExample : IExamplesProvider<Resource<ClienteResponse>>
    {
        public Resource<ClienteResponse> GetExamples()
        {
            var data = new ClienteResponse
            {
                Id = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"),
                Nome = "Ana Souza",
                Cpf = "12345678901",
                Telefone = "11988887777",
                Email = "ana.souza@example.com"
            };

            var links = new[]
            {
                new Link("self", "/api/Clientes/aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee", "GET"),
                new Link("update", "/api/Clientes/aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee", "PUT"),
                new Link("delete", "/api/Clientes/aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee", "DELETE"),
                new Link("collection", "/api/Clientes?page=1&pageSize=20", "GET")
            };

            return new Resource<ClienteResponse>(data, links);
        }
    }
}