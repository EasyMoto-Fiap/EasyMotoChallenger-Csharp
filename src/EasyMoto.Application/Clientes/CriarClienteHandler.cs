using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Clientes
{
    public sealed class CriarClienteHandler
    {
        private readonly IClienteRepository _repo;
        public CriarClienteHandler(IClienteRepository repo) => _repo = repo;

        public async Task<ClienteResponse> ExecuteAsync(CriarClienteRequest req, CancellationToken ct = default)
        {
            var exists = await _repo.ExistsCpfAsync(req.Cpf, null, ct);
            if (exists) throw new InvalidOperationException("Já existe cliente com este CPF.");

            var entity = new Cliente(req.Nome, req.Cpf, req.Telefone, req.Email);
            await _repo.AddAsync(entity, ct);

            return new ClienteResponse
            {
                Id = entity.Id,
                Nome = entity.NomeCliente,
                Cpf = entity.CpfCliente,
                Telefone = entity.TelefoneCliente,
                Email = entity.EmailCliente
            };
        }
    }
}