using EasyMoto.Application.Locacoes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Locacoes;

public sealed class CriarLocacaoHandler
{
    private readonly ILocacaoRepository _locacoes;
    private readonly IClienteRepository _clientes;

    public CriarLocacaoHandler(ILocacaoRepository locacoes, IClienteRepository clientes)
    {
        _locacoes = locacoes;
        _clientes = clientes;
    }

    public async Task<LocacaoResponse> Handle(CriarLocacaoRequest req, CancellationToken ct)
    {
        var cliente = await _clientes.GetByIdAsync(req.ClienteId, ct)
            ?? throw new InvalidOperationException("Cliente não encontrado");

        var loc = new Domain.Entities.Locacao(
            cliente.IdCliente,
            req.DataInicio,
            req.DataFim,
            req.StatusLocacao);

        await _locacoes.AddAsync(loc, ct);
        await _locacoes.SaveChangesAsync(ct);

        return new LocacaoResponse
        {
            IdLocacao = loc.IdLocacao,
            ClienteId = loc.ClienteId,
            DataInicio = loc.Periodo.Inicio,
            DataFim = loc.Periodo.Fim,
            StatusLocacao = loc.StatusLocacao
        };
    }
}
