using EasyMoto.Application.Locacoes.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Locacoes;

public sealed class CriarLocacaoHandler
{
    private readonly ILocacaoRepository _locacoes;
    private readonly IClienteRepository _clientes;
    private readonly IMotoRepository _motos;

    public CriarLocacaoHandler(
        ILocacaoRepository locacoes,
        IClienteRepository clientes,
        IMotoRepository motos)
    {
        _locacoes = locacoes;
        _clientes = clientes;
        _motos = motos;
    }

    public async Task<LocacaoResponse> Handle(CriarLocacaoRequest req, CancellationToken ct)
    {
        var cliente = await _clientes.GetByIdAsync(req.ClienteId, ct)
            ?? throw new InvalidOperationException("Cliente não encontrado");

        var moto = await _motos.GetByIdAsync(req.MotoId, ct)
            ?? throw new InvalidOperationException("Moto não encontrada");

        if (moto.Status != MotoStatus.Disponivel)
            throw new InvalidOperationException("Moto indisponível");

        var sobrepoe = await _locacoes.ExisteSobreposicaoAsync(req.MotoId, req.Inicio, req.Fim, ct);
        if (sobrepoe) throw new InvalidOperationException("Já existe locação para a moto no período");

        var loc = new Locacao(cliente.IdCliente, moto.IdMoto, req.Inicio, req.Fim, req.ValorDiaria);

        moto.Alugar();
        await _motos.UpdateAsync(moto, ct);
        await _locacoes.AddAsync(loc, ct);
        await _locacoes.SaveChangesAsync(ct);

        return new LocacaoResponse
        {
            IdLocacao = loc.IdLocacao,
            ClienteId = loc.ClienteId,
            MotoId = loc.MotoId,
            Inicio = loc.Periodo.Inicio,
            Fim = loc.Periodo.Fim,
            ValorDiaria = loc.ValorDiaria,
            ValorTotal = loc.ValorTotal,
            Status = loc.Status.ToString()
        };
    }
}
