using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;
using EasyMoto.Domain.ValueObjects;

namespace EasyMoto.Application.Clientes
{
    public class CriarClienteHandler
    {
        private readonly IClienteRepository _repo;

        public CriarClienteHandler(IClienteRepository repo)
        {
            _repo = repo;
        }

        public async Task<ClienteResponse> ExecuteAsync(CriarClienteRequest req, CancellationToken ct)
        {
            var cpf = Cpf.Create(req.Cpf);
            var telefone = new Telefone(req.Telefone);
            var email = new Email(req.Email);

            var entity = new Cliente(req.Nome, cpf, telefone, email);
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