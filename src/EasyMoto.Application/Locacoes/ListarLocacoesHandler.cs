using EasyMoto.Application.Locacoes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Locacoes;

public sealed class ListarLocacoesHandler
{
    private readonly ILocacaoRepository _repo;

    public ListarLocacoesHandler(ILocacaoRepository repo) => _repo = repo;

    public async Task<List<LocacaoResponse>> Handle(CancellationToken ct)
    {
        var list = await _repo.GetAllAsync(ct);
        return list.Select(l => new LocacaoResponse
        {
            IdLocacao = l.IdLocacao,
            ClienteId = l.ClienteId,
            MotoId = l.MotoId,
            Inicio = l.Periodo.Inicio,
            Fim = l.Periodo.Fim,
            ValorDiaria = l.ValorDiaria,
            ValorTotal = l.ValorTotal,
            Status = l.Status.ToString()
        }).ToList();
    }
}
