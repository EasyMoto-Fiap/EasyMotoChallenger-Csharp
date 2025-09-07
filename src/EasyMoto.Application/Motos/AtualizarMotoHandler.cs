using EasyMoto.Application.Motos.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Motos;

public sealed class AtualizarMotoHandler
{
    private readonly IMotoRepository _repo;

    public AtualizarMotoHandler(IMotoRepository repo) => _repo = repo;

    public async Task<MotoResponse?> Handle(int id, AtualizarMotoRequest req, CancellationToken ct)
    {
        var m = await _repo.GetByIdAsync(id, ct);
        if (m is null) return null;

        var modelo = req.Modelo ?? string.Empty;
        var placa = req.Placa ?? string.Empty;

        var novaPlacaNormalizada = placa.Trim().ToUpperInvariant();
        if (novaPlacaNormalizada != m.Placa && await _repo.ExistsByPlacaAsync(novaPlacaNormalizada, ct))
            throw new InvalidOperationException("Placa já cadastrada");

        m.SetModelo(modelo);
        m.SetPlaca(placa);
        m.SetAno(req.Ano);

        await _repo.UpdateAsync(m, ct);
        await _repo.SaveChangesAsync(ct);

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
