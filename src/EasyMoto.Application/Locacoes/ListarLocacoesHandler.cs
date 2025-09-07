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
            DataInicio = l.Periodo.Inicio,
            DataFim = l.Periodo.Fim,
            StatusLocacao = l.StatusLocacao
        }).ToList();
    }
}
