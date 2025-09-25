using EasyMoto.Application.Empresas.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Empresas;

public sealed class AtualizarEmpresaHandler
{
    private readonly IEmpresaRepository _repo;
    public AtualizarEmpresaHandler(IEmpresaRepository repo) => _repo = repo;

    public async Task<EmpresaResponse?> ExecuteAsync(int id, AtualizarEmpresaRequest req, CancellationToken ct = default)
    {
        var input = new Empresa(req.NomeEmpresa, req.Cnpj);
        var updated = await _repo.UpdateAsync(id, input, ct);
        if (updated is null) return null;
        return new EmpresaResponse(updated.IdEmpresa, updated.NomeEmpresa, updated.Cnpj);
    }
}