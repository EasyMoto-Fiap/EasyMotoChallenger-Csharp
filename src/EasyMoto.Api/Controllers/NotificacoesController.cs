using EasyMoto.Application.DTOs.Notificacoes;
using EasyMoto.Application.UseCases.Notificacoes.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers
{
    [ApiController]
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
        public async Task<IActionResult> Create([FromBody] CreateNotificacaoRequest request)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var r = await _create.Execute(request);
            r.Links = new Dictionary<string, string> { ["self"] = $"/api/notificacoes/{r.Id}", ["marcarLida"] = $"/api/notificacoes/{r.Id}/marcar-lida", ["delete"] = $"/api/notificacoes/{r.Id}" };
            return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var items = await _list.Execute(page, pageSize);
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var r = await _get.Execute(id);
            if (r == null) return NotFound();
            r.Links = new Dictionary<string, string> { ["self"] = $"/api/notificacoes/{r.Id}", ["marcarLida"] = $"/api/notificacoes/{r.Id}/marcar-lida", ["delete"] = $"/api/notificacoes/{r.Id}" };
            return Ok(r);
        }

        [HttpPost("{id:int}/marcar-lida")]
        public async Task<IActionResult> MarkAsRead(int id, [FromBody] MarkAsLidaRequest request)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var ok = await _mark.Execute(id, request.UsuarioId);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _delete.Execute(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
