using EasyMoto.Application.Clientes.Contracts;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Clientes
{
    public class CriarClienteRequestExample : IExamplesProvider<CriarClienteRequest>
    {
        public CriarClienteRequest GetExamples()
        {
            return new CriarClienteRequest
            {
                Nome = "Ana Souza",
                Cpf = "12345678901",
                Telefone = "11988887777",
                Email = "ana.souza@example.com"
            };
        }
    }
}