using EasyMoto.Application.Motos.Contracts;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Motos;

public class MotoResponseExample : IExamplesProvider<MotoResponse>
{
    public MotoResponse GetExamples() => new()
    {
        IdMoto = 1,
        Modelo = "Honda CG 160",
        Placa = "ABC1D23",
        Ano = 2023,
        Status = "Disponivel"
    };
}