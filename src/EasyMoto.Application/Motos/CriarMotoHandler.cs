using EasyMoto.Application.Motos.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Motos;

public sealed class CriarMotoHandler
{
    private readonly IMotoRepository _repo;
    public CriarMotoHandler(IMotoRepository repo) => _repo = repo;

    public async Task<MotoResponse> ExecuteAsync(CriarMotoRequest req, CancellationToken ct = default)
    {
        var placaEmUso = await _repo.ExistsPlacaAsync(req.Placa, ct);
        if (placaEmUso)
            throw new InvalidOperationException("Já existe uma moto com essa placa.");

        var entity = new Moto(
            req.Placa, req.Marca, req.Modelo, req.Cor,
            req.AnoFabricacao, req.Status, req.LocacaoId, req.LocalizacaoId);

        await _repo.AddAsync(entity, ct);

        return new MotoResponse
        {
            Id = entity.Id,
            Placa = entity.Placa,
            Marca = entity.Marca,
            Modelo = entity.Modelo,
            Cor = entity.Cor,
            AnoFabricacao = entity.AnoFabricacao,
            Status = entity.Status,
            LocacaoId = entity.LocacaoId,
            LocalizacaoId = entity.LocalizacaoId
        };
    }
}