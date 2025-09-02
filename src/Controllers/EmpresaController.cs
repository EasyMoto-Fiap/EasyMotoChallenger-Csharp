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
    public class EmpresaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EmpresaController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaResponseDto>>> GetEmpresas([FromQuery] string? nome)
        {
            var query = _context.Empresas.AsQueryable();
            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(e => e.NomeEmpresa.Contains(nome));
            var empresas = await query.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<EmpresaResponseDto>>(empresas));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpresaResponseDto>> GetEmpresa(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
                return NotFound();
            return Ok(_mapper.Map<EmpresaResponseDto>(empresa));
        }

        [HttpPost]
        public async Task<ActionResult<EmpresaResponseDto>> CreateEmpresa([FromBody] EmpresaRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var empresa = _mapper.Map<Empresa>(dto);
            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<EmpresaResponseDto>(empresa);
            return CreatedAtAction(nameof(GetEmpresa), new { id = response.IdEmpresa }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmpresa(int id, [FromBody] EmpresaRequestDto dto)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
                return NotFound();
            _mapper.Map(dto, empresa);
            _context.Entry(empresa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
                return NotFound();
            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
