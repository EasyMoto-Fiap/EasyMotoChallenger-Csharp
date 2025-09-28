using EasyMoto.Api.Infra.Hateoas;
using EasyMoto.Application.DTOs.Usuarios;
using EasyMoto.Application.UseCases.Usuarios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EasyMoto.Application.UseCases.Usuarios.Implementations;

namespace EasyMoto.Api.Controllers.Usuarios
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ICreateUsuarioUseCase _create;
        private readonly IGetUsuarioUseCase _get;
        private readonly IListUsuariosUseCase _list;
        private readonly IUpdateUsuarioUseCase _update;
        private readonly IDeleteUsuarioUseCase _delete;

        public UsuariosController(
            ICreateUsuarioUseCase create,
            IGetUsuarioUseCase get,
            IListUsuariosUseCase list,
            IUpdateUsuarioUseCase update,
            IDeleteUsuarioUseCase delete)
        {
            _create = create;
            _get = get;
            _list = list;
            _update = update;
            _delete = delete;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUsuarioRequest request)
        {
            var u = await _create.Execute(request);
            if (u == null) return Problem("Falha ao criar o usuário");

            u.Links = LinkBuilder.Build(
                self: $"/api/usuarios/{u.Id}",
                update: $"/api/usuarios/{u.Id}",
                delete: $"/api/usuarios/{u.Id}");

            return CreatedAtAction(nameof(GetById), new { id = u.Id }, u);
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
            var u = await _get.Execute(id);
            if (u == null) return NotFound();

            u.Links = LinkBuilder.Build(
                self: $"/api/usuarios/{u.Id}",
                update: $"/api/usuarios/{u.Id}",
                delete: $"/api/usuarios/{u.Id}");

            return Ok(u);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUsuarioRequest request)
        {
            var ok = await _update.Execute(id, request);
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
