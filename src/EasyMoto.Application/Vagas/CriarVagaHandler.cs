using EasyMoto.Application.Vagas.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Vagas;

public sealed class CriarVagaHandler
{
    private readonly IVagaRepository _repo;

    public CriarVagaHandler(IVagaRepository repo) => _repo = repo;

    public async Task<VagaResponse> ExecuteAsync(CriarVagaRequest req, CancellationToken ct = default)
    {
        var exists = await _repo.ExistsNumeroAsync(req.PatioId, req.NumeroVaga, null, ct);
        if (exists) throw new InvalidOperationException("Já existe uma vaga com esse número neste pátio.");

        var entity = new Vaga(req.PatioId, req.NumeroVaga, req.Ocupada, req.MotoId);
        await _repo.AddAsync(entity, ct);

        return new VagaResponse
        {
            Id = entity.Id,
            PatioId = entity.PatioId,
            NumeroVaga = entity.NumeroVaga,
            Ocupada = entity.Ocupada,
            MotoId = entity.MotoId
        };
    }
}