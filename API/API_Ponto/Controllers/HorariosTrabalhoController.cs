using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Ponto.Data;
using API_Ponto.Models;

namespace API_Ponto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorariosTrabalhoController : ControllerBase
    {
        private readonly API_PontoContext _context;

        public HorariosTrabalhoController(API_PontoContext context)
        {
            _context = context;
        }

        // GET: api/HorariosTrabalho
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetHorarioTrabalho()
        {
          if (_context.HorarioTrabalho == null)
          {
              return NotFound();
          }
            return await _context.HorarioTrabalho.Include(n => n.UsuariosHorario)
                .Select(n => new
                {
                    n.IdHorario,
                    n.HorarioEntrada,
                    n.HorarioPausa,
                    n.HorarioRetorno,
                    n.HorarioSaida,
                    UsuariosHorario = n.UsuariosHorario != null ? n.UsuariosHorario.Select(u => new
                    {
                        u.IdUsuario,
                        u.Nome,
                        u.Cargo,
                        u.Email
                    }).ToList() : null
                }).ToListAsync();
        }

        // GET: api/HorariosTrabalho/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetHorarioTrabalho(int id)
        {
          if (_context.HorarioTrabalho == null)
          {
              return NotFound();
          }
            var horarioTrabalho = await _context.HorarioTrabalho.Include(n => n.UsuariosHorario)
                .Select(n => new
                {
                    n.IdHorario,
                    n.HorarioEntrada,
                    n.HorarioPausa,
                    n.HorarioRetorno,
                    n.HorarioSaida,
                    UsuariosHorario = n.UsuariosHorario != null ? n.UsuariosHorario.Select(u => new
                    {
                        u.IdUsuario,
                        u.Nome,
                        u.Cargo,
                        u.Email
                    }).ToList() : null
                }).FirstOrDefaultAsync(n => n.IdHorario == id);

            if (horarioTrabalho == null)
            {
                return NotFound();
            }

            return Ok(horarioTrabalho);
        }

        // PUT: api/HorariosTrabalho/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorarioTrabalho(int id, HorarioTrabalho horarioTrabalho)
        {
            if (id != horarioTrabalho.IdHorario)
            {
                return BadRequest();
            }

            _context.Entry(horarioTrabalho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorarioTrabalhoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/HorariosTrabalho
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HorarioTrabalho>> PostHorarioTrabalho(HorarioTrabalho horarioTrabalho)
        {
          if (_context.HorarioTrabalho == null)
          {
              return Problem("Entity set 'API_PontoContext.HorarioTrabalho'  is null.");
          }
            _context.HorarioTrabalho.Add(horarioTrabalho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHorarioTrabalho", new { id = horarioTrabalho.IdHorario }, horarioTrabalho);
        }

        // DELETE: api/HorariosTrabalho/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorarioTrabalho(int id)
        {
            if (_context.HorarioTrabalho == null)
            {
                return NotFound();
            }
            var horarioTrabalho = await _context.HorarioTrabalho.FindAsync(id);
            if (horarioTrabalho == null)
            {
                return NotFound();
            }

            _context.HorarioTrabalho.Remove(horarioTrabalho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HorarioTrabalhoExists(int id)
        {
            return (_context.HorarioTrabalho?.Any(e => e.IdHorario == id)).GetValueOrDefault();
        }
    }
}
