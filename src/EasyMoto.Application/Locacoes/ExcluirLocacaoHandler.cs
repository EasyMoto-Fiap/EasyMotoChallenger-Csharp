using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Locacoes;

public sealed class ExcluirLocacaoHandler
{
    private readonly ILocacaoRepository _locacoes;

    public ExcluirLocacaoHandler(ILocacaoRepository locacoes) => _locacoes = locacoes;

    public async Task<bool> Handle(int id, CancellationToken ct)
    {
        var l = await _locacoes.GetByIdAsync(id, ct);
        if (l is null) return false;

        await _locacoes.DeleteAsync(id, ct);
        await _locacoes.SaveChangesAsync(ct);
        return true;
    }
}
