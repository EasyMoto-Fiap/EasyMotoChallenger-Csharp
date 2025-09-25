using EasyMoto.Application.Empresas.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Empresas;

public sealed class ObterEmpresaPorIdHandler
{
    private readonly IEmpresaRepository _repo;
    public ObterEmpresaPorIdHandler(IEmpresaRepository repo) => _repo = repo;

    public async Task<EmpresaResponse?> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var e = await _repo.GetByIdAsync(id, ct);
        if (e is null) return null;
        return new EmpresaResponse(e.IdEmpresa, e.NomeEmpresa, e.Cnpj);
    }
}