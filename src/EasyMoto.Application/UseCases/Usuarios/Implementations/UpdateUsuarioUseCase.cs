using EasyMoto.Application.DTOs.Usuarios;
using EasyMoto.Application.UseCases.Usuarios.Interfaces;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Usuarios.Implementations;

public class UpdateUsuarioUseCase : IUpdateUsuarioUseCase
{
    private readonly IUsuarioRepository _repo;

    public UpdateUsuarioUseCase(IUsuarioRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Execute(int id, UpdateUsuarioRequest request)
    {
        var e = await _repo.GetByIdAsync(id);
        if (e == null) return false;

        e.NomeCompleto = request.NomeCompleto;
        e.Email = request.Email;
        if (!string.IsNullOrWhiteSpace(request.SenhaHash))
            e.SenhaHash = request.SenhaHash!;
        e.Telefone = request.Telefone;
        e.Cpf = request.Cpf;
        e.CepFilial = request.CepFilial;
        e.Perfil = (PerfilUsuario)request.Perfil;
        e.Ativo = request.Ativo;
        e.FilialId = request.FilialId;

        await _repo.UpdateAsync(e);
        return true;
    }
}