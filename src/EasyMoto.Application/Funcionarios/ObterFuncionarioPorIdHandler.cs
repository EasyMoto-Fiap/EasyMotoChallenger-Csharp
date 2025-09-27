using EasyMoto.Application.Funcionarios.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Funcionarios;

public sealed class ObterFuncionarioPorIdHandler
{
    private readonly IFuncionarioRepository _repo;

    public ObterFuncionarioPorIdHandler(IFuncionarioRepository repo) => _repo = repo;

    public async Task<FuncionarioResponse?> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var e = await _repo.GetByIdAsync(id, ct);
        if (e is null) return null;
        return new FuncionarioResponse
        {
            Id = e.Id,
            NomeFuncionario = e.NomeFuncionario,
            Cpf = e.Cpf,
            FilialId = e.FilialId
        };
    }
}