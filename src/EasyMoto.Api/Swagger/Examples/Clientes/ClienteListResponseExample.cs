using EasyMoto.Application.Clientes.Contracts;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Clientes;

public class ClienteListResponseExample : IExamplesProvider<IEnumerable<ClienteResponse>>
{
    public IEnumerable<ClienteResponse> GetExamples() => new[]
    {
        new ClienteResponse
        {
            IdCliente = 1,
            NomeCliente = "Marcos Silva",
            CpfCliente = "04813257860",
            TelefoneCliente = "+55 11 98765-4321",
            EmailCliente = "marcos.silva@example.com"
        },
        new ClienteResponse
        {
            IdCliente = 2,
            NomeCliente = "Maria Souza",
            CpfCliente = "12345678901",
            TelefoneCliente = "+55 11 99876-5432",
            EmailCliente = "maria.souza@example.com"
        }
    };
}