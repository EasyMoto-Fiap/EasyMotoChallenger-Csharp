using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Clientes;

public sealed class AtualizarClienteHandler
{
    private readonly IClienteRepository _repo;

    public AtualizarClienteHandler(IClienteRepository repo) => _repo = repo;

    public async Task<ClienteResponse?> Handle(int id, AtualizarClienteRequest req, CancellationToken ct)
    {
        var c = await _repo.GetByIdAsync(id, ct);
        if (c is null) return null;

        c.SetNome(req.NomeCliente);
        c.SetCpf(req.CpfCliente);
        c.SetTelefone(req.TelefoneCliente);
        c.SetEmail(req.EmailCliente);

        await _repo.UpdateAsync(c, ct);
        await _repo.SaveChangesAsync(ct);

        return new ClienteResponse
        {
            IdCliente = c.IdCliente,
            NomeCliente = c.NomeCliente,
            CpfCliente = c.CpfCliente,
            TelefoneCliente = c.TelefoneCliente ?? "",
            EmailCliente = c.EmailCliente ?? ""
        };
    }
}
