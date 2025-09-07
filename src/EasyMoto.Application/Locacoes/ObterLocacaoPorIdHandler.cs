using EasyMoto.Application.Locacoes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Locacoes;

public sealed class ObterLocacaoPorIdHandler
{
    private readonly ILocacaoRepository _repo;

    public ObterLocacaoPorIdHandler(ILocacaoRepository repo) => _repo = repo;

    public async Task<LocacaoResponse?> Handle(int id, CancellationToken ct)
    {
        var l = await _repo.GetByIdAsync(id, ct);
        if (l is null) return null;

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
