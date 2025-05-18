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
    public class ClienteLocacaoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ClienteLocacaoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteLocacaoResponseDto>>> GetLocacoes([FromQuery] string? status)
        {
            var query = _context.ClienteLocacoes.AsQueryable();
            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(l => l.StatusLocacao.Contains(status));
            var locacoes = await query.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ClienteLocacaoResponseDto>>(locacoes));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteLocacaoResponseDto>> GetLocacao(int id)
        {
            var locacao = await _context.ClienteLocacoes.FindAsync(id);
            if (locacao == null)
                return NotFound();
            return Ok(_mapper.Map<ClienteLocacaoResponseDto>(locacao));
        }

        [HttpPost]
        public async Task<ActionResult<ClienteLocacaoResponseDto>> CreateLocacao([FromBody] ClienteLocacaoRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var locacao = _mapper.Map<ClienteLocacao>(dto);
            _context.ClienteLocacoes.Add(locacao);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<ClienteLocacaoResponseDto>(locacao);
            return CreatedAtAction(nameof(GetLocacao), new { id = response.IdLocacao }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocacao(int id, [FromBody] ClienteLocacaoRequestDto dto)
        {
            var locacao = await _context.ClienteLocacoes.FindAsync(id);
            if (locacao == null)
                return NotFound();
            _mapper.Map(dto, locacao);
            _context.Entry(locacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocacao(int id)
        {
            var locacao = await _context.ClienteLocacoes.FindAsync(id);
            if (locacao == null)
                return NotFound();
            _context.ClienteLocacoes.Remove(locacao);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
