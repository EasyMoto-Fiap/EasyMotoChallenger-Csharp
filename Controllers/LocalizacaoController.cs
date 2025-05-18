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
    public class LocalizacaoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LocalizacaoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocalizacaoResponseDto>>> GetLocalizacoes([FromQuery] string? status)
        {
            var query = _context.Localizacoes.AsQueryable();
            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(l => l.StatusLoc.Contains(status));
            var localizacoes = await query.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<LocalizacaoResponseDto>>(localizacoes));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocalizacaoResponseDto>> GetLocalizacao(int id)
        {
            var localizacao = await _context.Localizacoes.FindAsync(id);
            if (localizacao == null)
                return NotFound();
            return Ok(_mapper.Map<LocalizacaoResponseDto>(localizacao));
        }

        [HttpPost]
        public async Task<ActionResult<LocalizacaoResponseDto>> CreateLocalizacao([FromBody] LocalizacaoRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var localizacao = _mapper.Map<Localizacao>(dto);
            _context.Localizacoes.Add(localizacao);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<LocalizacaoResponseDto>(localizacao);
            return CreatedAtAction(nameof(GetLocalizacao), new { id = response.IdLocalizacao }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocalizacao(int id, [FromBody] LocalizacaoRequestDto dto)
        {
            var localizacao = await _context.Localizacoes.FindAsync(id);
            if (localizacao == null)
                return NotFound();
            _mapper.Map(dto, localizacao);
            _context.Entry(localizacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocalizacao(int id)
        {
            var localizacao = await _context.Localizacoes.FindAsync(id);
            if (localizacao == null)
                return NotFound();
            _context.Localizacoes.Remove(localizacao);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
