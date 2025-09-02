using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EasyMoto.src.Application.DTOs.Request;
using EasyMoto.src.Application.DTOs.Response;
using EasyMoto.src.Domain.Entities;
using EasyMoto.src.Infrastructure.Context;

namespace EasyMoto.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperadorController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OperadorController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperadorResponseDto>>> GetOperadores([FromQuery] string? nome)
        {
            var query = _context.Operadores.AsQueryable();
            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(o => o.NomeOpr.Contains(nome));
            var operadores = await query.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<OperadorResponseDto>>(operadores));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperadorResponseDto>> GetOperador(int id)
        {
            var operador = await _context.Operadores.FindAsync(id);
            if (operador == null)
                return NotFound();
            return Ok(_mapper.Map<OperadorResponseDto>(operador));
        }

        [HttpPost]
        public async Task<ActionResult<OperadorResponseDto>> CreateOperador([FromBody] OperadorRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var operador = _mapper.Map<Operador>(dto);
            _context.Operadores.Add(operador);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<OperadorResponseDto>(operador);
            return CreatedAtAction(nameof(GetOperador), new { id = response.IdOperador }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOperador(int id, [FromBody] OperadorRequestDto dto)
        {
            var operador = await _context.Operadores.FindAsync(id);
            if (operador == null)
                return NotFound();
            _mapper.Map(dto, operador);
            _context.Entry(operador).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperador(int id)
        {
            var operador = await _context.Operadores.FindAsync(id);
            if (operador == null)
                return NotFound();
            _context.Operadores.Remove(operador);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
