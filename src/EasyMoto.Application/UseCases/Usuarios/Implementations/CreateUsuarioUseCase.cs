using System.Security.Cryptography;
using System.Text;
using EasyMoto.Application.DTOs.Usuarios;
using EasyMoto.Application.UseCases.Usuarios.Interfaces;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Usuarios.Implementations;

public class CreateUsuarioUseCase : ICreateUsuarioUseCase
{
    private readonly IUsuarioRepository _repo;

    public CreateUsuarioUseCase(IUsuarioRepository repo) => _repo = repo;

    public async Task<UsuarioResponse> Execute(CreateUsuarioRequest request)
    {
        var senha = request.Senha.Trim();
        var confirmar = request.ConfirmarSenha.Trim();

        if (senha.Length == 0 || confirmar.Length == 0 || senha != confirmar)
            throw new ArgumentException("Senha e ConfirmarSenha são obrigatórias e devem coincidir.");

        using var sha = SHA256.Create();
        var senhaHash = Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(senha))).ToLowerInvariant();

        var entity = new Usuario
        {
            NomeCompleto = request.NomeCompleto,
            Email = request.Email,
            Telefone = request.Telefone,
            Cpf = request.Cpf,
            CepFilial = request.CepFilial,
            Perfil = (PerfilUsuario)request.Perfil,
            Ativo = request.Ativo,
            FilialId = request.FilialId,
            SenhaHash = senhaHash
        };

        await _repo.AddAsync(entity);

        return new UsuarioResponse
        {
            Id = entity.Id,
            NomeCompleto = entity.NomeCompleto,
            Email = entity.Email,
            Telefone = entity.Telefone,
            Cpf = entity.Cpf,
            CepFilial = entity.CepFilial,
            Perfil = (int)entity.Perfil,
            Ativo = entity.Ativo,
            FilialId = entity.FilialId
        };
    }
}