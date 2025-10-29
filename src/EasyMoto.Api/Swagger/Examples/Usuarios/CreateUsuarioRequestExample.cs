using EasyMoto.Application.DTOs.Usuarios;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Usuarios
{
    public class CreateUsuarioRequestExample : IExamplesProvider<CreateUsuarioRequest>
    {
        public CreateUsuarioRequest GetExamples() =>
            new CreateUsuarioRequest
            {
                NomeCompleto = "Ana Operadora",
                Email = "ana.operadora@example.com",
                Telefone = "11 99999-9999",
                Cpf = "12345678909",
                CepFilial = "01001-000",
                Senha = "SenhaForte@123",
                ConfirmarSenha = "SenhaForte@123",
                Perfil = 0,
                Ativo = true,
                FilialId = 1
            };
    }
}