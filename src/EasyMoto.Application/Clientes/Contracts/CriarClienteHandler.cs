using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Clientes;

public sealed class CriarClienteHandler
{
    private readonly IClienteRepository _repo;

    public CriarClienteHandler(IClienteRepository repo) => _repo = repo;

    public async Task<ClienteResponse> Handle(CriarClienteRequest req, CancellationToken ct)
    {
        if (await _repo.ExistsByCpfAsync(req.CpfCliente, ct)) throw new InvalidOperationException("CPF já existe");

        var cliente = new Cliente(req.NomeCliente, req.CpfCliente, req.TelefoneCliente, req.EmailCliente);
        await _repo.AddAsync(cliente, ct);
        await _repo.SaveChangesAsync(ct);

        var salvo = await _repo.GetByIdAsync(cliente.IdCliente, ct) ?? cliente;

        return new ClienteResponse
        {
            IdCliente = salvo.IdCliente,
            NomeCliente = salvo.NomeCliente,
            CpfCliente = salvo.CpfCliente,
            TelefoneCliente = salvo.TelefoneCliente,
            EmailCliente = salvo.EmailCliente
        };
    }
}
