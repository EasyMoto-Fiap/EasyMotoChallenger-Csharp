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

        l.DefinirPeriodo(req.Inicio, req.Fim);
        l.DefinirValorDiaria(req.ValorDiaria);

        await _repo.UpdateAsync(l, ct);
        await _repo.SaveChangesAsync(ct);

        return new LocacaoResponse
        {
            IdLocacao = l.IdLocacao,
            ClienteId = l.ClienteId,
            MotoId = l.MotoId,
            Inicio = l.Periodo.Inicio,
            Fim = l.Periodo.Fim,
            ValorDiaria = l.ValorDiaria,
            ValorTotal = l.ValorTotal,
            Status = l.Status.ToString()
        };
    }
}
