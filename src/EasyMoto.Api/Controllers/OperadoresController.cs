using EasyMoto.Application.Operadores;
using EasyMoto.Application.Operadores.Contracts;
using EasyMoto.Application.Shared.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperadoresController : ControllerBase
    {
        private readonly CriarOperadorHandler _criar;
        private readonly AtualizarOperadorHandler _atualizar;
        private readonly ObterOperadorPorIdHandler _obter;
        private readonly ListarOperadoresHandler _listar;
        private readonly ExcluirOperadorHandler _excluir;

        public OperadoresController(
            CriarOperadorHandler criar,
            AtualizarOperadorHandler atualizar,
            ObterOperadorPorIdHandler obter,
            ListarOperadoresHandler listar,
            ExcluirOperadorHandler excluir)
        {
            _criar = criar;
            _atualizar = atualizar;
            _obter = obter;
            _listar = listar;
            _excluir = excluir;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<OperadorResponse>>> Get([FromQuery] PageQuery query, CancellationToken ct)
        {
            var result = await _listar.ExecuteAsync(query, ct);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OperadorResponse>> GetById(int id, CancellationToken ct)
        {
            var result = await _obter.ExecuteAsync(id, ct);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<OperadorResponse>> Post([FromBody] CriarOperadorRequest request, CancellationToken ct)
        {
            var created = await _criar.ExecuteAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] AtualizarOperadorRequest request, CancellationToken ct)
        {
            if (id != request.Id) return BadRequest();
            var ok = await _atualizar.ExecuteAsync(request, ct);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await _excluir.ExecuteAsync(id, ct);
            return NoContent();
        }
    }
}
