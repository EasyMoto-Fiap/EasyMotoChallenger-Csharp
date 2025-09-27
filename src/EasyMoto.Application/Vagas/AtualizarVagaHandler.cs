using EasyMoto.Application.Vagas.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Vagas;

public sealed class AtualizarVagaHandler
{
    private readonly IVagaRepository _repo;

    public AtualizarVagaHandler(IVagaRepository repo) => _repo = repo;

    public async Task<bool> ExecuteAsync(int id, AtualizarVagaRequest req, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        if (entity is null) return false;

        var duplicada = await _repo.ExistsNumeroAsync(entity.PatioId, req.NumeroVaga, id, ct);
        if (duplicada) throw new InvalidOperationException("Já existe uma vaga com esse número neste pátio.");

        entity.Update(entity.PatioId, req.NumeroVaga, req.Ocupada, req.MotoId);

        await _repo.UpdateAsync(entity, ct);
        return true;
    }
}