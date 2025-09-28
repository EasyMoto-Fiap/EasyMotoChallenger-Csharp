using EasyMoto.Application.DTOs.Motos;
using EasyMoto.Application.UseCases.Motos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyMoto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotosController : ControllerBase
    {
        private readonly ICreateMotoUseCase _create;
        private readonly IGetMotoUseCase _get;
        private readonly IListMotosUseCase _list;
        private readonly IUpdateMotoUseCase _update;
        private readonly IDeleteMotoUseCase _delete;

        public MotosController(ICreateMotoUseCase create, IGetMotoUseCase get, IListMotosUseCase list, IUpdateMotoUseCase update, IDeleteMotoUseCase delete)
        {
            _create = create;
            _get = get;
            _list = list;
            _update = update;
            _delete = delete;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMotoRequest request)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var m = await _create.Execute(request);
            m.Links = new Dictionary<string, string> { ["self"] = $"/api/motos/{m.Id}", ["update"] = $"/api/motos/{m.Id}", ["delete"] = $"/api/motos/{m.Id}" };
            return CreatedAtAction(nameof(GetById), new { id = m.Id }, m);
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
            var m = await _get.Execute(id);
            if (m == null) return NotFound();
            m.Links = new Dictionary<string, string> { ["self"] = $"/api/motos/{m.Id}", ["update"] = $"/api/motos/{m.Id}", ["delete"] = $"/api/motos/{m.Id}" };
            return Ok(m);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMotoRequest request)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var m = await _update.Execute(id, request);
            if (m == null) return NotFound();
            m.Links = new Dictionary<string, string> { ["self"] = $"/api/motos/{m.Id}", ["update"] = $"/api/motos/{m.Id}", ["delete"] = $"/api/motos/{m.Id}" };
            return Ok(m);
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
