using EasyMoto.Application.Motos.Contracts;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Motos;

public class CriarMotoRequestExample : IExamplesProvider<CriarMotoRequest>
{
    public CriarMotoRequest GetExamples() => new()
    {
        Modelo = "Honda CG 160",
        Placa = "ABC1D23",
        Ano = 2023
    };
}