using EasyMoto.Application.Motos.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Motos;

public sealed class CriarMotoHandler
{
    private readonly IMotoRepository _repo;

    public CriarMotoHandler(IMotoRepository repo) => _repo = repo;

    public async Task<MotoResponse> Handle(CriarMotoRequest req, CancellationToken ct)
    {
        if (await _repo.ExistsByPlacaAsync(req.Placa, ct)) throw new InvalidOperationException("Placa já cadastrada");

        var moto = new Moto(req.Modelo, req.Placa, req.Ano);
        await _repo.AddAsync(moto, ct);
        await _repo.SaveChangesAsync(ct);

        var salvo = await _repo.GetByIdAsync(moto.IdMoto, ct) ?? moto;

        return new MotoResponse
        {
            IdMoto = salvo.IdMoto,
            Modelo = salvo.Modelo,
            Placa = salvo.Placa,
            Ano = salvo.Ano,
            Status = salvo.Status.ToString()
        };
    }
}
