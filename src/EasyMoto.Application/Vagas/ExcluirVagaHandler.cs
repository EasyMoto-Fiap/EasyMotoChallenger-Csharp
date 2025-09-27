using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Vagas;

public sealed class ExcluirVagaHandler
{
    private readonly IVagaRepository _repo;

    public ExcluirVagaHandler(IVagaRepository repo) => _repo = repo;

    public Task ExecuteAsync(int id, CancellationToken ct) =>
        _repo.DeleteAsync(id, ct);
}