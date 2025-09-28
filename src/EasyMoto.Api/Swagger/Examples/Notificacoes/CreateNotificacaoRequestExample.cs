using EasyMoto.Application.DTOs.Notificacoes;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Notificacoes

{
    public class CreateNotificacaoRequestExample : IExamplesProvider<CreateNotificacaoRequest>
    {
        public CreateNotificacaoRequest GetExamples() => new CreateNotificacaoRequest
        {
            Tipo = 0,
            Mensagem = "Moto cadastrada",
            MotoId = 1,
            UsuarioOrigemId = 3,
            Escopo = 0
        };
    }
}