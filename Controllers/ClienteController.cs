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
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ClienteController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteResponseDto>>> GetClientes([FromQuery] string? nome)
        {
            var query = _context.Clientes.AsQueryable();
            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(c => c.NomeCliente.Contains(nome));
            var clientes = await query.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ClienteResponseDto>>(clientes));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteResponseDto>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound();
            return Ok(_mapper.Map<ClienteResponseDto>(cliente));
        }

        [HttpPost]
        public async Task<ActionResult<ClienteResponseDto>> CreateCliente([FromBody] ClienteRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var cliente = _mapper.Map<Cliente>(dto);
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<ClienteResponseDto>(cliente);
            return CreatedAtAction(nameof(GetCliente), new { id = response.IdCliente }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] ClienteRequestDto dto)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound();
            _mapper.Map(dto, cliente);
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound();
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
