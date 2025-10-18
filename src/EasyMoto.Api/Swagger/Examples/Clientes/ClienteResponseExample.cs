using EasyMoto.Application.Clientes.Contracts;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Clientes;

public class ClienteResponseExample : IExamplesProvider<ClienteResponse>
{
    public ClienteResponse GetExamples() => new()
    {
        IdCliente = 1,
        NomeCliente = "Marcos Silva",
        CpfCliente = "04813257860",
        TelefoneCliente = "+55 11 98765-4321",
        EmailCliente = "marcos.silva@example.com"
    };
}