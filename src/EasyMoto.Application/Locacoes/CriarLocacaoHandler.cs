using EasyMoto.Application.Locacoes.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Domain.ValueObjects;

namespace EasyMoto.Application.Locacoes;

public sealed class CriarLocacaoHandler
{
    private readonly ILocacaoRepository _repo;

    public CriarLocacaoHandler(ILocacaoRepository repo)
    {
        _repo = repo;
    }

    public async Task<LocacaoResponse> ExecuteAsync(CriarLocacaoRequest request, CancellationToken ct = default)
    {
        var periodo = Periodo.Create(request.Inicio, request.Fim);
        var locacao = new Locacao(Guid.NewGuid(), request.ClienteId, request.MotoId, periodo);
        await _repo.AddAsync(locacao, ct);
        return new LocacaoResponse { Id = locacao.Id, ClienteId = locacao.ClienteId, MotoId = locacao.MotoId, Inicio = locacao.Periodo.Inicio, Fim = locacao.Periodo.Fim };
    }
}