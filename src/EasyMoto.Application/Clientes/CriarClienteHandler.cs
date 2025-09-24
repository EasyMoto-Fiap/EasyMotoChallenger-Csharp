using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Domain.ValueObjects;

namespace EasyMoto.Application.Clientes
{
    public sealed class CriarClienteHandler
    {
        private readonly IClienteRepository _repo;
        public CriarClienteHandler(IClienteRepository repo) => _repo = repo;

        public async Task<ClienteResponse> ExecuteAsync(CriarClienteRequest req, CancellationToken ct = default)
        {
            var entity = new Cliente(req.Nome, new Cpf(req.Cpf), new Telefone(req.Telefone), new Email(req.Email));
            await _repo.AddAsync(entity, ct);

            return new ClienteResponse
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Cpf = entity.Cpf.Value,
                Telefone = entity.Telefone.Value,
                Email = entity.Email.Value
            };
        }
    }
}