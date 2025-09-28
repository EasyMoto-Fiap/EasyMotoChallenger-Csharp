using EasyMoto.Application.DTOs.Legendas;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Legendas
{
    public class CreateLegendaStatusRequestExample : IExamplesProvider<CreateLegendaStatusRequest>
    {
        public CreateLegendaStatusRequest GetExamples() => new CreateLegendaStatusRequest
        {
            Titulo = "Disponível",
            Descricao = "Moto pronta para uso",
            CorHex = "#28A745",
            Ativo = true
        };
    }
}