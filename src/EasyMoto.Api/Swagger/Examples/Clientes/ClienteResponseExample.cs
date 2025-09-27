using EasyMoto.Application.Clientes.Contracts;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Clientes;

public sealed class ClienteResponseExample : IExamplesProvider<ClienteResponse>
{
    public ClienteResponse GetExamples() => new ClienteResponse
    {
        Id = 1,
        Nome = "João Silva",
        Cpf = "12345678901",
        Telefone = "11988887777",
        Email = "joao.silva@example.com"
    };
}