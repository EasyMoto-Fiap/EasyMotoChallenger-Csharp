using EasyMoto.Application.DTOs.Legendas;
using EasyMoto.Application.UseCases.Legendas.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers
{
    [ApiController]
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
        public async Task<IActionResult> Create([FromBody] CreateLegendaStatusRequest request)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var r = await _create.Execute(request);
            r.Links = new Dictionary<string, string> { ["self"] = $"/api/legendasstatus/{r.Id}", ["update"] = $"/api/legendasstatus/{r.Id}", ["delete"] = $"/api/legendasstatus/{r.Id}" };
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
            r.Links = new Dictionary<string, string> { ["self"] = $"/api/legendasstatus/{r.Id}", ["update"] = $"/api/legendasstatus/{r.Id}", ["delete"] = $"/api/legendasstatus/{r.Id}" };
            return Ok(r);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLegendaStatusRequest request)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var r = await _update.Execute(id, request);
            if (r == null) return NotFound();
            r.Links = new Dictionary<string, string> { ["self"] = $"/api/legendasstatus/{r.Id}", ["update"] = $"/api/legendasstatus/{r.Id}", ["delete"] = $"/api/legendasstatus/{r.Id}" };
            return Ok(r);
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
