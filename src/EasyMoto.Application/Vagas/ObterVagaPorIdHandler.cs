using EasyMoto.Application.Vagas.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Vagas;

public sealed class ObterVagaPorIdHandler
{
    private readonly IVagaRepository _repo;

    public ObterVagaPorIdHandler(IVagaRepository repo) => _repo = repo;

    public async Task<VagaResponse?> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var e = await _repo.GetByIdAsync(id, ct);
        if (e is null) return null;

        return new VagaResponse
        {
            Id = e.Id,
            PatioId = e.PatioId,
            NumeroVaga = e.NumeroVaga,
            Ocupada = e.Ocupada,
            MotoId = e.MotoId
        };
    }
}