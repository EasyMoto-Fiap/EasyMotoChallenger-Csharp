using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Clientes;

public sealed class ExcluirClienteHandler
{
    private readonly IClienteRepository _repo;

    public ExcluirClienteHandler(IClienteRepository repo) => _repo = repo;

    public async Task<bool> Handle(int id, CancellationToken ct)
    {
        var c = await _repo.GetByIdAsync(id, ct);
        if (c is null) return false;
        await _repo.DeleteAsync(id, ct);
        await _repo.SaveChangesAsync(ct);
        return true;
    }
}
