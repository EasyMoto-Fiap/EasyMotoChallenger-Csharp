using Microsoft.AspNetCore.Mvc;
using EasyMoto.Infrastructure.Context;
using EasyMoto.Domain.Entities;
using EasyMoto.Application.DTOs.Request;
using EasyMoto.Application.DTOs.Response;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace EasyMoto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FuncionarioController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuncionarioResponseDto>>> GetFuncionarios([FromQuery] string? nome)
        {
            var query = _context.Funcionarios.AsQueryable();
            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(f => f.NomeFunc.Contains(nome));
            var funcionarios = await query.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<FuncionarioResponseDto>>(funcionarios));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FuncionarioResponseDto>> GetFuncionario(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
                return NotFound();
            return Ok(_mapper.Map<FuncionarioResponseDto>(funcionario));
        }

        [HttpPost]
        public async Task<ActionResult<FuncionarioResponseDto>> CreateFuncionario([FromBody] FuncionarioRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var funcionario = _mapper.Map<Funcionario>(dto);
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<FuncionarioResponseDto>(funcionario);
            return CreatedAtAction(nameof(GetFuncionario), new { id = response.IdFunc }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFuncionario(int id, [FromBody] FuncionarioRequestDto dto)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
                return NotFound();
            _mapper.Map(dto, funcionario);
            _context.Entry(funcionario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuncionario(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
                return NotFound();
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
