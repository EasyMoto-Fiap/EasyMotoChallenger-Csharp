using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.ClienteLocacoes;

public sealed class ExcluirClienteLocacaoHandler
{
    private readonly IClienteLocacaoRepository _repo;

    public ExcluirClienteLocacaoHandler(IClienteLocacaoRepository repo) => _repo = repo;

    public async Task<bool> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var e = await _repo.GetByIdAsync(id, ct);
        if (e is null) return false;

        await _repo.DeleteAsync(e, ct);
        return true;
    }
}