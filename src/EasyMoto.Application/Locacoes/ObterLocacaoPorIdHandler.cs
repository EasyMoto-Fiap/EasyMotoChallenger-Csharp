using EasyMoto.Application.Locacoes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Locacoes;

public sealed class ObterLocacaoPorIdHandler
{
    private readonly ILocacaoRepository _repo;
    public ObterLocacaoPorIdHandler(ILocacaoRepository repo) => _repo = repo;

    public async Task<LocacaoResponse?> ExecuteAsync(Guid id, CancellationToken ct = default)
    {
        var l = await _repo.GetByIdAsync(id, ct);
        return l is null ? null : new LocacaoResponse { Id = l.Id, ClienteId = l.ClienteId, MotoId = l.MotoId, Inicio = l.Periodo.Inicio, Fim = l.Periodo.Fim };
    }
}
