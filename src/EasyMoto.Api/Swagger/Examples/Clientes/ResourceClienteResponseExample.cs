using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Application.Shared.Pagination;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Clientes;

public sealed class ResourceClienteResponseExample : IExamplesProvider<PagedResult<ClienteResponse>>
{
    public PagedResult<ClienteResponse> GetExamples()
    {
        var items = new List<ClienteResponse>
        {
            new ClienteResponse
            {
                Id = 1,
                Nome = "João Silva",
                Cpf = "12345678901",
                Telefone = "11988887777",
                Email = "joao.silva@example.com"
            },
            new ClienteResponse
            {
                Id = 2,
                Nome = "Maria Souza",
                Cpf = "98765432100",
                Telefone = "21999996666",
                Email = "maria.souza@example.com"
            }
        };

        return new PagedResult<ClienteResponse>(items, 1, 10, 2);
    }
}