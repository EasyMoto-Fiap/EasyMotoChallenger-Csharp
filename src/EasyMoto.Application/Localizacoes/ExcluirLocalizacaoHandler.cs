using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Localizacoes;

public sealed class ExcluirLocalizacaoHandler
{
    private readonly ILocalizacaoRepository _repo;

    public ExcluirLocalizacaoHandler(ILocalizacaoRepository repo) => _repo = repo;

    public async Task<bool> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        if (entity is null) return false;

        await _repo.DeleteAsync(id, ct);
        return true;
    }
}