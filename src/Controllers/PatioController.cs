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
    public class PatioController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PatioController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatioResponseDto>>> GetPatios([FromQuery] string? nome)
        {
            var query = _context.Patios.AsQueryable();
            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(p => p.NomePatio.Contains(nome));
            var patios = await query.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<PatioResponseDto>>(patios));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatioResponseDto>> GetPatio(int id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();
            return Ok(_mapper.Map<PatioResponseDto>(patio));
        }

        [HttpPost]
        public async Task<ActionResult<PatioResponseDto>> CreatePatio([FromBody] PatioRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var patio = _mapper.Map<Patio>(dto);
            _context.Patios.Add(patio);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<PatioResponseDto>(patio);
            return CreatedAtAction(nameof(GetPatio), new { id = response.IdPatio }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatio(int id, [FromBody] PatioRequestDto dto)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();
            _mapper.Map(dto, patio);
            _context.Entry(patio).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatio(int id)
        {
            var patio = await _context.Patios.FindAsync(id);
            if (patio == null)
                return NotFound();
            _context.Patios.Remove(patio);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
