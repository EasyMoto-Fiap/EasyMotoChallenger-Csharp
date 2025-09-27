using EasyMoto.Application.ClienteLocacoes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.ClienteLocacoes;

public sealed class AtualizarClienteLocacaoHandler
{
    private readonly IClienteLocacaoRepository _repo;

    public AtualizarClienteLocacaoHandler(IClienteLocacaoRepository repo) => _repo = repo;

    public async Task<bool> ExecuteAsync(int id, AtualizarClienteLocacaoRequest request, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct);
        if (entity is null) return false;

        entity.Atualizar(request.ClienteId, request.MotoId, request.DataInicio, request.DataFim, request.StatusLocacao);
        await _repo.UpdateAsync(entity, ct);
        return true;
    }
}