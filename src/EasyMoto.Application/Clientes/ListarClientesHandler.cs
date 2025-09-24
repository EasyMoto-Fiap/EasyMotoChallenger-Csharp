using EasyMoto.Application.Clientes.Contracts;
using EasyMoto.Application.Shared.Pagination;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Clientes
{
    public sealed class ListarClientesHandler
    {
        private readonly IClienteRepository _repo;
        public ListarClientesHandler(IClienteRepository repo) => _repo = repo;

        public async Task<PagedResult<ClienteResponse>> ExecuteAsync(PageQuery query, CancellationToken ct = default)
        {
            var total = await _repo.CountAsync(ct);
            var items = await _repo.ListAsync(query.Page, query.Size, ct);
            var result = items.Select(c => new ClienteResponse
            {
                Id = c.Id,
                Nome = c.Nome,
                Cpf = c.Cpf.Value,
                Telefone = c.Telefone.Value,
                Email = c.Email.Value
            });
            return new PagedResult<ClienteResponse>(result, query.Page, query.Size, total);
        }
    }
}