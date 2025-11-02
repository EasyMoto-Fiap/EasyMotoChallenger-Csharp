using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Api.Controllers.Auth;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly IUsuarioRepository _usuarios;

    public AuthController(IUsuarioRepository usuarios) => _usuarios = usuarios;

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest? request)
    {
        if (request is null)
            return BadRequest(new { message = "Corpo da requisição inválido." });

        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
            return BadRequest(new { message = "Email e senha são obrigatórios." });

        var email = request.Email.Trim();
        var user = await _usuarios.ObterPorEmailAsync(email);
        if (user is null || !user.Ativo)
            return Unauthorized(new { message = "Email ou senha inválidos" });

        var senhaHash = Sha256Hex(request.Senha);
        if (!string.Equals(user.SenhaHash, senhaHash, StringComparison.Ordinal))
            return Unauthorized(new { message = "Email ou senha inválidos" });

        var resp = new LoginResponse
        {
            Token = Guid.NewGuid().ToString("N"),
            Usuario = new UsuarioDto
            {
                Id = user.Id,
                NomeCompleto = user.NomeCompleto,
                Email = user.Email,
                Telefone = user.Telefone,
                Cpf = user.Cpf,
                CepFilial = user.CepFilial,
                Perfil = (int)user.Perfil,
                Ativo = user.Ativo,
                FilialId = user.FilialId
            }
        };

        return Ok(resp);
    }

    private static string Sha256Hex(string value)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(value));
        var sb = new StringBuilder(bytes.Length * 2);
        foreach (var b in bytes) sb.Append(b.ToString("x2"));
        return sb.ToString();
    }
}

public sealed class LoginResponse
{
    public string Token { get; init; } = string.Empty;
    public UsuarioDto Usuario { get; init; } = new UsuarioDto();
}

public sealed class UsuarioDto
{
    public int Id { get; init; }
    public string NomeCompleto { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Telefone { get; init; } = string.Empty;
    public string Cpf { get; init; } = string.Empty;
    public string CepFilial { get; init; } = string.Empty;
    public int Perfil { get; init; }
    public bool Ativo { get; init; }
    public int FilialId { get; init; }
}
