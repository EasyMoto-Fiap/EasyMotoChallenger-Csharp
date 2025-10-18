using EasyMoto.Application.Motos.Contracts;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Motos;

public class MotoListResponseExample : IExamplesProvider<IEnumerable<MotoResponse>>
{
    public IEnumerable<MotoResponse> GetExamples() => new[]
    {
        new MotoResponse
        {
            IdMoto = 1,
            Modelo = "Honda CG 160",
            Placa = "ABC1D23",
            Ano = 2023,
            Status = "Disponivel"
        },
        new MotoResponse
        {
            IdMoto = 2,
            Modelo = "Yamaha Fazer 250",
            Placa = "XYZ9Z99",
            Ano = 2022,
            Status = "EmUso"
        }
    };
}