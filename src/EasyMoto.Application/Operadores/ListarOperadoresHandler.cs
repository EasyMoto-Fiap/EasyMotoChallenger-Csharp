using EasyMoto.Application.Operadores.Contracts;
using EasyMoto.Application.Shared.Pagination;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Operadores
{
    public class ListarOperadoresHandler
    {
        private readonly IOperadorRepository _repo;

        public ListarOperadoresHandler(IOperadorRepository repo)
        {
            _repo = repo;
        }

        public async Task<PagedResult<OperadorResponse>> ExecuteAsync(PageQuery query, CancellationToken ct)
        {
            var items = await _repo.ListAsync(query.Page, query.Size, ct);
            var total = items.Count;
            var data = items.Select(o => new OperadorResponse(o.Id, o.NomeOperador, o.Cpf, o.Telefone, o.Email, o.FilialId));
            return new PagedResult<OperadorResponse>(data, query.Page, query.Size, total);
        }
    }
}