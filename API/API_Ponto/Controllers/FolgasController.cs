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
    public class FolgasController : ControllerBase
    {
        private readonly API_PontoContext _context;

        public FolgasController(API_PontoContext context)
        {
            _context = context;
        }

        // GET: api/Folgas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetFolga()
        {
          if (_context.Folga == null)
          {
              return NotFound();
          }
            return await _context.Folga.Include(n => n.UsuariosFolga)
                .Select(n => new
                {
                    n.IdFolga,
                    n.DiaSemana,
                    UsuariosFolga = n.UsuariosFolga != null ? n.UsuariosFolga.Select(u => new
                    {
                        u.IdUsuario,
                        u.Nome,
                        u.Cargo,
                        u.Email
                    }).ToList() : null
                }).ToListAsync();
        }

        // GET: api/Folgas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetFolga(int id)
        {
          if (_context.Folga == null)
          {
              return NotFound();
          }
            var folga = await _context.Folga.Include(n => n.UsuariosFolga)
                .Select(n => new
                {
                    n.IdFolga,
                    n.DiaSemana,
                    UsuariosFolga = n.UsuariosFolga != null ? n.UsuariosFolga.Select(u => new
                    {
                        u.IdUsuario,
                        u.Nome,
                        u.Cargo,
                        u.Email
                    }).ToList() : null
                }).FirstOrDefaultAsync(n => n.IdFolga == id);

            if (folga == null)
            {
                return NotFound();
            }

            return Ok(folga);
        }

        // PUT: api/Folgas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFolga(int id, Folga folga)
        {
            if (id != folga.IdFolga)
            {
                return BadRequest();
            }

            _context.Entry(folga).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FolgaExists(id))
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

        // POST: api/Folgas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Folga>> PostFolga(Folga folga)
        {
          if (_context.Folga == null)
          {
              return Problem("Entity set 'API_PontoContext.Folga'  is null.");
          }
            _context.Folga.Add(folga);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFolga", new { id = folga.IdFolga }, folga);
        }

        // DELETE: api/Folgas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFolga(int id)
        {
            if (_context.Folga == null)
            {
                return NotFound();
            }
            var folga = await _context.Folga.FindAsync(id);
            if (folga == null)
            {
                return NotFound();
            }

            _context.Folga.Remove(folga);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FolgaExists(int id)
        {
            return (_context.Folga?.Any(e => e.IdFolga == id)).GetValueOrDefault();
        }
    }
}
