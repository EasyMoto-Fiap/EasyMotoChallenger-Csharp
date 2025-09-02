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
    public class FilialController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FilialController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilialResponseDto>>> GetFiliais([FromQuery] string? nome)
        {
            var query = _context.Filiais.AsQueryable();
            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(f => f.NomeFilial.Contains(nome));
            var filiais = await query.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<FilialResponseDto>>(filiais));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FilialResponseDto>> GetFilial(int id)
        {
            var filial = await _context.Filiais.FindAsync(id);
            if (filial == null)
                return NotFound();
            return Ok(_mapper.Map<FilialResponseDto>(filial));
        }

        [HttpPost]
        public async Task<ActionResult<FilialResponseDto>> CreateFilial([FromBody] FilialRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var filial = _mapper.Map<Filial>(dto);
            _context.Filiais.Add(filial);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<FilialResponseDto>(filial);
            return CreatedAtAction(nameof(GetFilial), new { id = response.IdFilial }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFilial(int id, [FromBody] FilialRequestDto dto)
        {
            var filial = await _context.Filiais.FindAsync(id);
            if (filial == null)
                return NotFound();
            _mapper.Map(dto, filial);
            _context.Entry(filial).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilial(int id)
        {
            var filial = await _context.Filiais.FindAsync(id);
            if (filial == null)
                return NotFound();
            _context.Filiais.Remove(filial);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
