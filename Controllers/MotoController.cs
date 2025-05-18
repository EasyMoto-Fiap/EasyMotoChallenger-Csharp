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
    public class MotoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MotoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotoResponseDto>>> GetMotos([FromQuery] string? status)
        {
            var query = _context.Motos.AsQueryable();
            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(m => m.StatusMoto.Contains(status));
            var motos = await query.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<MotoResponseDto>>(motos));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MotoResponseDto>> GetMoto(int id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null)
                return NotFound();
            return Ok(_mapper.Map<MotoResponseDto>(moto));
        }

        [HttpPost]
        public async Task<ActionResult<MotoResponseDto>> CreateMoto([FromBody] MotoRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var moto = _mapper.Map<Moto>(dto);
            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<MotoResponseDto>(moto);
            return CreatedAtAction(nameof(GetMoto), new { id = response.IdMoto }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMoto(int id, [FromBody] MotoRequestDto dto)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null)
                return NotFound();
            _mapper.Map(dto, moto);
            _context.Entry(moto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoto(int id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null)
                return NotFound();
            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
