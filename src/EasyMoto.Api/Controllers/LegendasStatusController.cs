using EasyMoto.Application.DTOs.Legendas;
using EasyMoto.Application.UseCases.Legendas.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EasyMoto.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    public class LegendasStatusController : ControllerBase
    {
        private readonly ICreateLegendaStatusUseCase _create;
        private readonly IGetLegendaStatusUseCase _get;
        private readonly IListLegendasStatusUseCase _list;
        private readonly IUpdateLegendaStatusUseCase _update;
        private readonly IDeleteLegendaStatusUseCase _delete;

        public LegendasStatusController(ICreateLegendaStatusUseCase create, IGetLegendaStatusUseCase get, IListLegendasStatusUseCase list, IUpdateLegendaStatusUseCase update, IDeleteLegendaStatusUseCase delete)
        {
            _create = create;
            _get = get;
            _list = list;
            _update = update;
            _delete = delete;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma legenda de status", Description = "Cria uma nova legenda de status.")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateLegendaStatusRequest request)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var r = await _create.Execute(request);
            r.Links = new Dictionary<string, string> { ["self"] = $"/api/legendasstatus/{r.Id}", ["update"] = $"/api/legendasstatus/{r.Id}", ["delete"] = $"/api/legendasstatus/{r.Id}" };
            return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista legendas de status", Description = "Retorna uma lista paginada de legendas de status.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var items = await _list.Execute(page, pageSize);
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Obtém legenda de status por ID", Description = "Retorna os detalhes de uma legenda de status existente.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var r = await _get.Execute(id);
            if (r == null) return NotFound();
            r.Links = new Dictionary<string, string> { ["self"] = $"/api/legendasstatus/{r.Id}", ["update"] = $"/api/legendasstatus/{r.Id}", ["delete"] = $"/api/legendasstatus/{r.Id}" };
            return Ok(r);
        }

        [HttpPut("{id:int}")]
        [SwaggerOperation(Summary = "Atualiza legenda de status", Description = "Atualiza os dados da legenda de status informada.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLegendaStatusRequest request)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var r = await _update.Execute(id, request);
            if (r == null) return NotFound();
            r.Links = new Dictionary<string, string> { ["self"] = $"/api/legendasstatus/{r.Id}", ["update"] = $"/api/legendasstatus/{r.Id}", ["delete"] = $"/api/legendasstatus/{r.Id}" };
            return Ok(r);
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Remove legenda de status", Description = "Exclui a legenda de status informada.")]
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
