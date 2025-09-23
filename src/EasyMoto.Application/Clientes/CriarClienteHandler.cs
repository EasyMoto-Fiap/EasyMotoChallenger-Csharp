using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Domain.ValueObjects;

namespace EasyMoto.Application.Clientes;

public sealed class CriarClienteHandler
{
    private readonly IClienteRepository _repo;
    public CriarClienteHandler(IClienteRepository repo) => _repo = repo;

    public async Task<ClienteResponse> ExecuteAsync(CriarClienteRequest request, CancellationToken ct = default)
    {
        var cliente = new Cliente(Guid.NewGuid(), request.Nome, Cpf.From(request.Cpf));
        await _repo.AddAsync(cliente, ct);
        return new ClienteResponse { Id = cliente.Id, Nome = cliente.Nome, Cpf = cliente.Cpf.Value };
    }
}