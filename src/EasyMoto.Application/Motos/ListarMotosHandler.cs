using EasyMoto.Application.Motos.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Motos;

public sealed class ListarMotosHandler
{
    private readonly IMotoRepository _repo;

    public ListarMotosHandler(IMotoRepository repo) => _repo = repo;

    public async Task<List<MotoResponse>> Handle(CancellationToken ct)
    {
        var list = await _repo.GetAllAsync(ct);
        return list.Select(m => new MotoResponse
        {
            IdMoto = m.IdMoto,
            Modelo = m.Modelo,
            Placa = m.Placa,
            Ano = m.Ano,
            Status = m.Status.ToString()
        }).ToList();
    }
}
