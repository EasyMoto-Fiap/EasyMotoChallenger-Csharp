using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Locacoes;

public sealed class ExcluirLocacaoHandler
{
    private readonly ILocacaoRepository _locacoes;
    private readonly IMotoRepository _motos;

    public ExcluirLocacaoHandler(ILocacaoRepository locacoes, IMotoRepository motos)
    {
        _locacoes = locacoes;
        _motos = motos;
    }

    public async Task<bool> Handle(int id, CancellationToken ct)
    {
        var l = await _locacoes.GetByIdAsync(id, ct);
        if (l is null) return false;

        if (l.Status == LocacaoStatus.Aberta)
        {
            var m = await _motos.GetByIdAsync(l.MotoId, ct);
            if (m is not null && m.Status == MotoStatus.Alugada)
            {
                m.Devolver();
                await _motos.UpdateAsync(m, ct);
            }
        }

        await _locacoes.DeleteAsync(id, ct);
        await _locacoes.SaveChangesAsync(ct);
        return true;
    }
}
