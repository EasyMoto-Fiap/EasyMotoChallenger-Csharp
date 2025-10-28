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
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
            return BadRequest(new { message = "Email e senha são obrigatórios." });
        
        var perfil = request.Email.Contains("admin", StringComparison.OrdinalIgnoreCase) ? 1 : 0;

        var resp = new LoginResponse
        {
            Token = Guid.NewGuid().ToString("N"),
            Usuario = new UsuarioDto
            {
                Id = 1,
                NomeCompleto = perfil == 1 ? "Admin EasyMoto" : "Operador EasyMoto",
                Email = request.Email,
                Telefone = "11999999999",
                Cpf = "00000000000",
                CepFilial = "01001-000",
                Perfil = perfil,  
                Ativo = true,
                FilialId = 1
            }
        };

        return Ok(resp);
    }
}

public sealed class LoginResponse
{
    public string Token { get; set; } = default!;
    public UsuarioDto Usuario { get; set; } = default!;
}

public sealed class UsuarioDto
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Telefone { get; set; } = default!;
    public string Cpf { get; set; } = default!;
    public string CepFilial { get; set; } = default!;
    public int Perfil { get; set; } 
    public bool Ativo { get; set; }
    public int FilialId { get; set; }
}
