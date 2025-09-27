using EasyMoto.Application.Motos.Contracts;
using EasyMoto.Application.Shared.Pagination;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Motos;

public sealed class ListarMotosHandler
{
    private readonly IMotoRepository _repo;
    public ListarMotosHandler(IMotoRepository repo) => _repo = repo;

    public async Task<PagedResult<MotoResponse>> ExecuteAsync(PageQuery query, CancellationToken ct = default)
    {
        var total = await _repo.CountAsync(ct);
        var items = await _repo.ListAsync(query.Page, query.Size, ct);

        var mapped = items.Select(e => new MotoResponse
        {
            Id = e.Id,
            Placa = e.Placa,
            Marca = e.Marca,
            Modelo = e.Modelo,
            Cor = e.Cor,
            AnoFabricacao = e.AnoFabricacao,
            Status = e.Status,
            LocacaoId = e.LocacaoId,
            LocalizacaoId = e.LocalizacaoId
        }).ToList();

        return new PagedResult<MotoResponse>(mapped, total, query.Page, query.Size);
    }
}