using EasyMoto.Application.DTOs.Filiais;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Filiais;

public sealed class CreateFilialRequestExample : IExamplesProvider<CreateFilialRequest>
{
    public CreateFilialRequest GetExamples() => new CreateFilialRequest
    {
        Nome = "Filial Centro",
        Cep = "01001-000",
        Cidade = "São Paulo",
        Uf = "SP"
    };
}