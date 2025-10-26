using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers.Auth;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
            return Task.FromResult<IActionResult>(BadRequest(new { message = "Email e senha são obrigatórios." }));

        return Task.FromResult<IActionResult>(Ok(new LoginResponse { Message = "OK" }));
    }
}

public sealed class LoginResponse
{
    public string Message { get; set; } = "OK";
}