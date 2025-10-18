using EasyMoto.Application.Locacoes.Contracts;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Locacoes;

public class AtualizarLocacaoRequestExample : IExamplesProvider<AtualizarLocacaoRequest>
{
    public AtualizarLocacaoRequest GetExamples() => new()
    {
        DataInicio = new DateTime(2025, 9, 16, 9, 0, 0),
        DataFim = new DateTime(2025, 9, 19, 18, 0, 0),
        StatusLocacao = "Fechada"
    };
}