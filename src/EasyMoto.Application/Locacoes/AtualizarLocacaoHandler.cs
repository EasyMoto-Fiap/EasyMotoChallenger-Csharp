using EasyMoto.Application.Locacoes.Contracts;
using EasyMoto.Domain.Repositories;
using EasyMoto.Domain.ValueObjects;

namespace EasyMoto.Application.Locacoes;

public sealed class AtualizarLocacaoHandler
{
    private readonly ILocacaoRepository _repo;
    public AtualizarLocacaoHandler(ILocacaoRepository repo) => _repo = repo;

    public async Task<LocacaoResponse?> ExecuteAsync(Guid id, AtualizarLocacaoRequest request, CancellationToken ct = default)
    {
        var existente = await _repo.GetByIdAsync(id, ct);
        if (existente is null) return null;
        var periodo = Periodo.Create(request.Inicio, request.Fim);
        var nova = new Domain.Entities.Locacao(existente.Id, existente.ClienteId, existente.MotoId, periodo);
        await _repo.UpdateAsync(nova, ct);
        return new LocacaoResponse { Id = nova.Id, ClienteId = nova.ClienteId, MotoId = nova.MotoId, Inicio = periodo.Inicio, Fim = periodo.Fim };
    }
}