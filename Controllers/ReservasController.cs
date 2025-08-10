using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservaSalaAPI.Data;
using ReservaSalaAPI.Models;

namespace ReservaSalaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReservasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetReservas(
    [FromQuery] int page = 0,
    [FromQuery] int size = 10,
    [FromQuery] string? q = null)
    {
        var query = _context.Reservas.AsQueryable();

        // Filtro de busca
        if (!string.IsNullOrWhiteSpace(q))
        {
            var lower = q.ToLower();
            query = query.Where(r =>
                r.Local.ToLower().Contains(lower) ||
                r.Sala.ToLower().Contains(lower) ||
                r.Responsavel.ToLower().Contains(lower) ||
                r.Descricao.ToLower().Contains(lower)
            );
        }

        var total = await query.CountAsync();

        var data = await query
            .OrderBy(r => r.Id)
            .Skip(page * size)
            .Take(size)
            .ToListAsync();

        return Ok(new
        {
            total,
            data
        });
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Reserva>> GetReserva(int id)
    {
        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva == null)
            return NotFound();

        return reserva;
    }

    [HttpPost]
    public async Task<ActionResult<Reserva>> PostReserva(Reserva reserva)
    {
        // Validar choque de horário aqui (opcional, mas recomendado)
        try
        {
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
            return Ok(reserva);
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new { error = ex.InnerException?.Message ?? ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutReserva(int id, Reserva reserva)
    {
        if (id != reserva.Id)
            return BadRequest();

        _context.Entry(reserva).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Reservas.Any(e => e.Id == id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReserva(int id)
    {
        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva == null)
            return NotFound();

        _context.Reservas.Remove(reserva);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}