using EasyMoto.Application.DTOs.Notificacoes;
using EasyMoto.Application.UseCases.Notificacoes.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EasyMoto.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    public class NotificacoesController : ControllerBase
    {
        private readonly ICreateNotificacaoUseCase _create;
        private readonly IGetNotificacaoUseCase _get;
        private readonly IListNotificacoesUseCase _list;
        private readonly IMarkNotificacaoLidaUseCase _mark;
        private readonly IDeleteNotificacaoUseCase _delete;

        public NotificacoesController(ICreateNotificacaoUseCase create, IGetNotificacaoUseCase get, IListNotificacoesUseCase list, IMarkNotificacaoLidaUseCase mark, IDeleteNotificacaoUseCase delete)
        {
            _create = create;
            _get = get;
            _list = list;
            _mark = mark;
            _delete = delete;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma notificação", Description = "Cria uma nova notificação.")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateNotificacaoRequest request)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var r = await _create.Execute(request);
            r.Links = new Dictionary<string, string> { ["self"] = $"/api/notificacoes/{r.Id}", ["marcarLida"] = $"/api/notificacoes/{r.Id}/marcar-lida", ["delete"] = $"/api/notificacoes/{r.Id}" };
            return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista notificações", Description = "Retorna uma lista paginada de notificações.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var items = await _list.Execute(page, pageSize);
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Obtém notificação por ID", Description = "Retorna os detalhes de uma notificação existente.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var r = await _get.Execute(id);
            if (r == null) return NotFound();
            r.Links = new Dictionary<string, string> { ["self"] = $"/api/notificacoes/{r.Id}", ["marcarLida"] = $"/api/notificacoes/{r.Id}/marcar-lida", ["delete"] = $"/api/notificacoes/{r.Id}" };
            return Ok(r);
        }

        [HttpPost("{id:int}/marcar-lida")]
        [SwaggerOperation(Summary = "Marca notificação como lida", Description = "Marca a notificação informada como lida.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MarkAsRead(int id, [FromBody] MarkAsLidaRequest request)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var ok = await _mark.Execute(id, request.UsuarioId);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Remove notificação", Description = "Exclui a notificação informada.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _delete.Execute(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
