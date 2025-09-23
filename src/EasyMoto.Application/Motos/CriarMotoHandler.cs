using EasyMoto.Application.Motos.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Motos;

public sealed class CriarMotoHandler
{
    private readonly IMotoRepository _repo;
    public CriarMotoHandler(IMotoRepository repo) => _repo = repo;

    public async Task<MotoResponse> ExecuteAsync(CriarMotoRequest request, CancellationToken ct = default)
    {
        var moto = new Moto(Guid.NewGuid(), request.Placa);
        await _repo.AddAsync(moto, ct);
        return new MotoResponse { Id = moto.Id, Placa = moto.Placa, Status = moto.Status };
    }
}