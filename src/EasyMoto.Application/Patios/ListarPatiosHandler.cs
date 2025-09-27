using EasyMoto.Application.Patios.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Patios;

public sealed class ListarPatiosHandler
{
    private readonly IPatioRepository _repo;
    public ListarPatiosHandler(IPatioRepository repo) => _repo = repo;

    public async Task<IReadOnlyList<PatioResponse>> ExecuteAsync(CancellationToken ct = default)
    {
        var list = await _repo.ListAsync(ct);
        return list.Select(e => new PatioResponse
        {
            Id = e.Id,
            NomePatio = e.NomePatio,
            TamanhoPatio = e.TamanhoPatio,
            Andar = e.Andar,
            FilialId = e.FilialId
        }).ToList();
    }
}