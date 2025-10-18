using EasyMoto.Application.Locacoes.Contracts;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Locacoes;

public class LocacaoListResponseExample : IExamplesProvider<IEnumerable<LocacaoResponse>>
{
    public IEnumerable<LocacaoResponse> GetExamples() => new[]
    {
        new LocacaoResponse
        {
            IdLocacao = 1,
            ClienteId = 1,
            DataInicio = new DateTime(2025, 9, 15, 8, 0, 0),
            DataFim = new DateTime(2025, 9, 18, 18, 0, 0),
            StatusLocacao = "Aberta"
        },
        new LocacaoResponse
        {
            IdLocacao = 2,
            ClienteId = 2,
            DataInicio = new DateTime(2025, 10, 1, 9, 0, 0),
            DataFim = new DateTime(2025, 10, 3, 17, 0, 0),
            StatusLocacao = "Fechada"
        }
    };
}