using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Empresas;

public sealed class ExcluirEmpresaHandler
{
    private readonly IEmpresaRepository _repo;
    public ExcluirEmpresaHandler(IEmpresaRepository repo) => _repo = repo;
    public Task<bool> ExecuteAsync(int id, CancellationToken ct = default) => _repo.DeleteAsync(id, ct);
}