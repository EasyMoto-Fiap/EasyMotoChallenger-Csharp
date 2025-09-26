using EasyMoto.Application.Clientes.Contracts;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Clientes
{
    public class ClienteResponseExample : IExamplesProvider<ClienteResponse>
    {
        public ClienteResponse GetExamples()
        {
            return new ClienteResponse
            {
                Id = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"),
                Nome = "Ana Souza",
                Cpf = "12345678901",
                Telefone = "11988887777",
                Email = "ana.souza@example.com"
            };
        }
    }
}
