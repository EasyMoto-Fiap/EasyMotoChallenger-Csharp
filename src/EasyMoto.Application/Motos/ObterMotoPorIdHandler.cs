using EasyMoto.Application.Motos.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Motos;

public sealed class ObterMotoPorIdHandler
{
    private readonly IMotoRepository _repo;
    public ObterMotoPorIdHandler(IMotoRepository repo) => _repo = repo;

    public async Task<MotoResponse?> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var e = await _repo.GetByIdAsync(id, ct);
        if (e is null) return null;

        return new MotoResponse
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
        };
    }
}