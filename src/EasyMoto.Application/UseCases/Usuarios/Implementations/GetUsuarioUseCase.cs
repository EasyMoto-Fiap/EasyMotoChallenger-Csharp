using EasyMoto.Application.DTOs.Usuarios;
using EasyMoto.Application.UseCases.Usuarios.Interfaces;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Usuarios.Implementations;

public class GetUsuarioUseCase : IGetUsuarioUseCase
{
    private readonly IUsuarioRepository _repo;

    public GetUsuarioUseCase(IUsuarioRepository repo)
    {
        _repo = repo;
    }

    public async Task<UsuarioResponse?> Execute(int id)
    {
        var e = await _repo.GetByIdAsync(id);
        if (e == null) return null;

        return new UsuarioResponse
        {
            Id = e.Id,
            NomeCompleto = e.NomeCompleto,
            Email = e.Email,
            Telefone = e.Telefone,
            Cpf = e.Cpf,
            CepFilial = e.CepFilial,
            Perfil = (int)e.Perfil,
            Ativo = e.Ativo,
            FilialId = e.FilialId
        };
    }
}