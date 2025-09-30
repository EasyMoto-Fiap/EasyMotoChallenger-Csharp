using Microsoft.AspNetCore.Mvc;
using EasyMoto.Domain.Repositories;
using System.Security.Cryptography;
using System.Text;
using EasyMoto.Application.DTOs.Usuarios;

namespace EasyMoto.Api.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _repo;

        public AuthController(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public class LoginRequest
        {
            public string Email { get; set; } = default!;
            public string Senha { get; set; } = default!;
        }

        public class LoginResponse
        {
            public string Token { get; set; } = default!;
            public UsuarioResponse Usuario { get; set; } = default!;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await _repo.ObterPorEmailAsync(request.Email);
            if (usuario == null || !usuario.Ativo) return Unauthorized();

            using var sha = SHA256.Create();
            var senhaHash = Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(request.Senha))).ToLowerInvariant();
            if (senhaHash != usuario.SenhaHash) return Unauthorized();

            var response = new LoginResponse
            {
                Token = Guid.NewGuid().ToString("N"),
                Usuario = new UsuarioResponse
                {
                    Id = usuario.Id,
                    NomeCompleto = usuario.NomeCompleto,
                    Email = usuario.Email,
                    Telefone = usuario.Telefone,
                    Cpf = usuario.Cpf,
                    CepFilial = usuario.CepFilial,
                    Perfil = (int)usuario.Perfil,
                    Ativo = usuario.Ativo,
                    FilialId = usuario.FilialId
                }
            };

            return Ok(response);
        }
    }
}
