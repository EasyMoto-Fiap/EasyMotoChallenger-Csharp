using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Funcionarios;

public sealed class ExcluirFuncionarioHandler
{
    private readonly IFuncionarioRepository _repo;

    public ExcluirFuncionarioHandler(IFuncionarioRepository repo) => _repo = repo;

    public async Task<bool> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        if (entity is null) return false;
        await _repo.DeleteAsync(entity, ct);
        return true;
    }
}