using EasyMoto.Application.Patios.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Patios;

public sealed class CriarPatioHandler
{
    private readonly IFilialRepository _filiais;
    private readonly IPatioRepository _patios;

    public CriarPatioHandler(IFilialRepository filiais, IPatioRepository patios)
    {
        _filiais = filiais;
        _patios = patios;
    }

    public async Task<Patio> ExecuteAsync(CriarPatioRequest req, CancellationToken ct = default)
    {
        var filial = await _filiais.GetByIdAsync(req.FilialId, ct);
        if (filial is null) throw new ArgumentException("Filial não encontrada", nameof(req.FilialId));

        var patio = new Patio(
            req.NomePatio.Trim(),
            req.TamanhoPatio.Trim(),
            req.Andar.Trim(),
            req.FilialId
        );

        await _patios.AddAsync(patio, ct);
        return patio;
    }
}