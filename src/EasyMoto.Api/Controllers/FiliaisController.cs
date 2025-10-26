using EasyMoto.Api.Infra.Hateoas;
using EasyMoto.Application.DTOs.Filiais;
using EasyMoto.Application.UseCases.Filiais;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using EasyMoto.Api.Swagger.Examples.Filiais;

namespace EasyMoto.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    public class FiliaisController : ControllerBase
    {
        private readonly ICreateFilialUseCase _create;
        private readonly IGetFilialUseCase _get;
        private readonly IListFiliaisUseCase _list;
        private readonly IUpdateFilialUseCase _update;
        private readonly IDeleteFilialUseCase _delete;

        public FiliaisController(
            ICreateFilialUseCase create,
            IGetFilialUseCase get,
            IListFiliaisUseCase list,
            IUpdateFilialUseCase update,
            IDeleteFilialUseCase delete)
        {
            _create = create;
            _get = get;
            _list = list;
            _update = update;
            _delete = delete;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma filial", Description = "Cria uma nova filial.")]
        [SwaggerRequestExample(typeof(CreateFilialRequest), typeof(CreateFilialRequestExample))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateFilialRequest request)
        {
            var filial = await _create.Execute(request);
            filial.Links = LinkBuilder.Build(
                self:   $"/api/filiais/{filial.Id}",
                update: $"/api/filiais/{filial.Id}",
                delete: $"/api/filiais/{filial.Id}"
            );
            return CreatedAtAction(nameof(GetById), new { id = filial.Id }, filial);
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
            var filial = await _get.Execute(id);
            if (filial == null) return NotFound();

            filial.Links = LinkBuilder.Build(
                self:   $"/api/filiais/{id}",
                update: $"/api/filiais/{id}",
                delete: $"/api/filiais/{id}"
            );
            return Ok(filial);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFilialRequest request)
        {
            var ok = await _update.Execute(id, request);
            if (!ok) return NotFound();

            var filial = await _get.Execute(id);
            if (filial == null) return NotFound();

            filial.Links = LinkBuilder.Build(
                self:   $"/api/filiais/{id}",
                update: $"/api/filiais/{id}",
                delete: $"/api/filiais/{id}"
            );
            return Ok(filial);
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
