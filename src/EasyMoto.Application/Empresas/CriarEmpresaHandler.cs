using EasyMoto.Application.Empresas.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Empresas;

public sealed class CriarEmpresaHandler
{
    private readonly IEmpresaRepository _repo;
    public CriarEmpresaHandler(IEmpresaRepository repo) => _repo = repo;

    public async Task<EmpresaResponse> ExecuteAsync(CriarEmpresaRequest req, CancellationToken ct = default)
    {
        var entity = new Empresa(req.NomeEmpresa, req.Cnpj);
        entity = await _repo.AddAsync(entity, ct);
        return new EmpresaResponse(entity.IdEmpresa, entity.NomeEmpresa, entity.Cnpj);
    }
}