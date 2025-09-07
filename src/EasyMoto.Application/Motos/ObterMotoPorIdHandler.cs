using EasyMoto.Application.Motos.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Motos;

public sealed class ObterMotoPorIdHandler
{
    private readonly IMotoRepository _repo;

    public ObterMotoPorIdHandler(IMotoRepository repo) => _repo = repo;

    public async Task<MotoResponse?> Handle(int id, CancellationToken ct)
    {
        var m = await _repo.GetByIdAsync(id, ct);
        if (m is null) return null;

        return new MotoResponse
        {
            IdMoto = m.IdMoto,
            Modelo = m.Modelo,
            Placa = m.Placa,
            Ano = m.Ano,
            Status = m.Status.ToString()
        };
    }
}
