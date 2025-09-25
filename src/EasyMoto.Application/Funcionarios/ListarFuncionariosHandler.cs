using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyMoto.Application.Funcionarios.Contracts;
using EasyMoto.Application.Shared.Pagination;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Application.Funcionarios
{
    public sealed class ListarFuncionariosHandler
    {
        private readonly IFuncionarioRepository _repo;
        public ListarFuncionariosHandler(IFuncionarioRepository repo) => _repo = repo;

        public async Task<PagedResult<FuncionarioResponse>> ExecuteAsync(PageQuery query, CancellationToken ct = default)
        {
            var total = await _repo.CountAsync(ct);
            var items = await _repo.ListAsync(query.Page, query.Size, ct);
            var result = items.Select(e => new FuncionarioResponse
            {
                IdFuncionario = e.Id,
                NomeFuncionario = e.NomeFuncionario,
                Cpf = e.Cpf,
                FilialId = e.FilialId
            }).ToList();

            return new PagedResult<FuncionarioResponse>(result, query.Page, query.Size, total);
        }
    }
}