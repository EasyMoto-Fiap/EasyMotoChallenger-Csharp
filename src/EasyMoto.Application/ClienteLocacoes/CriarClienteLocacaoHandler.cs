using EasyMoto.Application.ClienteLocacoes.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.ClienteLocacoes;

public sealed class CriarClienteLocacaoHandler
{
    private readonly IClienteLocacaoRepository _repo;

    public CriarClienteLocacaoHandler(IClienteLocacaoRepository repo) => _repo = repo;

    public async Task<ClienteLocacaoResponse> ExecuteAsync(CriarClienteLocacaoRequest request, CancellationToken ct = default)
    {
        var entity = new ClienteLocacao(request.ClienteId, request.MotoId, request.DataInicio, request.DataFim, request.StatusLocacao);
        await _repo.AddAsync(entity, ct);

        return new ClienteLocacaoResponse
        {
            Id = entity.Id,
            ClienteId = entity.ClienteId,
            MotoId = entity.MotoId,
            DataInicio = entity.DataInicio,
            DataFim = entity.DataFim,
            StatusLocacao = entity.StatusLocacao
        };
    }
}