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
    public class VagaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public VagaController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VagaResponseDto>>> GetVagas([FromQuery] string? status)
        {
            var query = _context.Vagas.AsQueryable();
            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(v => v.StatusVaga.Contains(status));
            var vagas = await query.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<VagaResponseDto>>(vagas));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VagaResponseDto>> GetVaga(int id)
        {
            var vaga = await _context.Vagas.FindAsync(id);
            if (vaga == null)
                return NotFound();
            return Ok(_mapper.Map<VagaResponseDto>(vaga));
        }

        [HttpPost]
        public async Task<ActionResult<VagaResponseDto>> CreateVaga([FromBody] VagaRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var vaga = _mapper.Map<Vaga>(dto);
            _context.Vagas.Add(vaga);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<VagaResponseDto>(vaga);
            return CreatedAtAction(nameof(GetVaga), new { id = response.IdVaga }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVaga(int id, [FromBody] VagaRequestDto dto)
        {
            var vaga = await _context.Vagas.FindAsync(id);
            if (vaga == null)
                return NotFound();
            _mapper.Map(dto, vaga);
            _context.Entry(vaga).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaga(int id)
        {
            var vaga = await _context.Vagas.FindAsync(id);
            if (vaga == null)
                return NotFound();
            _context.Vagas.Remove(vaga);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
