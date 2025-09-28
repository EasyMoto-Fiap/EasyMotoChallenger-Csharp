using EasyMoto.Application.DTOs.Motos;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Motos
{
    public class CreateMotoRequestExample : IExamplesProvider<CreateMotoRequest>
    {
        public CreateMotoRequest GetExamples() => new CreateMotoRequest
        {
            Placa = "DEF2E34",
            Modelo = "Yamaha Fazer 250",
            Ano = 2023,
            Cor = "Azul",
            Ativo = true,
            FilialId = 3,
            Categoria = 1,
            StatusOperacional = 0,
            LegendaStatusId = 1,
            QrCode = "MOTO-DEF2E34"
        };
    }
}