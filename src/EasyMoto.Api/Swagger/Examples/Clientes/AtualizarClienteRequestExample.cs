using EasyMoto.Application.Clientes.Contracts;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Clientes;

public class AtualizarClienteRequestExample : IExamplesProvider<AtualizarClienteRequest>
{
    public AtualizarClienteRequest GetExamples() => new()
    {
        NomeCliente = "Marcos S. Silva",
        CpfCliente = "04813257860",
        TelefoneCliente = "+55 11 97654-3210",
        EmailCliente = "marcos.silva+atualizado@example.com"
    };
}