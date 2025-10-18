using EasyMoto.Application.Motos.Contracts;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Motos;

public class AtualizarMotoRequestExample : IExamplesProvider<AtualizarMotoRequest>
{
    public AtualizarMotoRequest GetExamples() => new()
    {
        Modelo = "Honda CG 160 Titan",
        Placa = "ABC1D23",
        Ano = 2024
    };
}