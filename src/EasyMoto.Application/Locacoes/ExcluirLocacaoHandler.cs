using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Locacoes;

public sealed class ExcluirLocacaoHandler
{
    private readonly ILocacaoRepository _repo;
    public ExcluirLocacaoHandler(ILocacaoRepository repo) => _repo = repo;

    public async Task<bool> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var existente = await _repo.GetByIdAsync(id, ct);
        if (existente is null) return false;
        await _repo.DeleteAsync(existente, ct);
        return true;
    }
}