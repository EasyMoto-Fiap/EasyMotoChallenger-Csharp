using EasyMoto.Application.UseCases.Usuarios.Interfaces;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.UseCases.Usuarios.Implementations;

public class DeleteUsuarioUseCase : IDeleteUsuarioUseCase
{
    private readonly IUsuarioRepository _repo;

    public DeleteUsuarioUseCase(IUsuarioRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Execute(int id)
    {
        var e = await _repo.GetByIdAsync(id);
        if (e == null) return false;

        await _repo.DeleteAsync(e);
        return true;
    }
}