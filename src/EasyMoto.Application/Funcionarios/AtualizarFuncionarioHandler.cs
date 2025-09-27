using EasyMoto.Application.Funcionarios.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Funcionarios;

public sealed class AtualizarFuncionarioHandler
{
    private readonly IFuncionarioRepository _repo;

    public AtualizarFuncionarioHandler(IFuncionarioRepository repo) => _repo = repo;

    public async Task<bool> ExecuteAsync(AtualizarFuncionarioRequest req, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(req.Id, ct);
        if (entity is null) return false;
        entity.Atualizar(req.NomeFuncionario, req.Cpf, req.FilialId);
        await _repo.UpdateAsync(entity, ct);
        return true;
    }
}