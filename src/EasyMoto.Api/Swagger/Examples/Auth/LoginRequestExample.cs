using EasyMoto.Api.Controllers.Auth;
using Swashbuckle.AspNetCore.Filters;

namespace EasyMoto.Api.Swagger.Examples.Auth;

public sealed class LoginRequestExample : IExamplesProvider<LoginRequest>
{
    public LoginRequest GetExamples() => new LoginRequest
    {
        Email = "usuario@dominio.com",
        Senha = "Senha@123"
    };
}