namespace EasyMoto.Api.Controllers.Auth;

public sealed class LoginRequest
{
    public string? Email { get; set; }
    public string? Senha { get; set; }
}