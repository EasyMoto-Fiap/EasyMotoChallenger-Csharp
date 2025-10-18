using EasyMoto.Application.Locacoes.Contracts;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Locacoes;

public class CriarLocacaoRequestExample : IExamplesProvider<CriarLocacaoRequest>
{
    public CriarLocacaoRequest GetExamples() => new()
    {
        ClienteId = 1,
        DataInicio = new DateTime(2025, 9, 15, 8, 0, 0),
        DataFim = new DateTime(2025, 9, 18, 18, 0, 0),
        StatusLocacao = "Aberta"
    };
}