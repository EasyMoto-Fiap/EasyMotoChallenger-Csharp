using EasyMoto.Application.Locacoes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Locacoes;

public sealed class AtualizarLocacaoHandler
{
    private readonly ILocacaoRepository _repo;

    public AtualizarLocacaoHandler(ILocacaoRepository repo) => _repo = repo;

    public async Task<LocacaoResponse?> Handle(int id, AtualizarLocacaoRequest req, CancellationToken ct)
    {
        var l = await _repo.GetByIdAsync(id, ct);
        if (l is null) return null;

        l.DefinirPeriodo(req.DataInicio, req.DataFim);
        l.DefinirStatus(req.StatusLocacao);

        await _repo.UpdateAsync(l, ct);
        await _repo.SaveChangesAsync(ct);

        return new LocacaoResponse
        {
            IdLocacao = l.IdLocacao,
            ClienteId = l.ClienteId,
            DataInicio = l.Periodo.Inicio,
            DataFim = l.Periodo.Fim,
            StatusLocacao = l.StatusLocacao
        };
    }
}
