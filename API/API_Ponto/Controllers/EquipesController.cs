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
    public class EquipesController : ControllerBase
    {
        private readonly API_PontoContext _context;

        public EquipesController(API_PontoContext context)
        {
            _context = context;
        }

        // GET: api/Equipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetEquipe()
        {
          if (_context.Equipe == null)
          {
              return NotFound();
          }
            return await _context.Equipe.Include(n => n.UsuariosEquipe)
                .Select(n => new
                {
                    n.IdEquipe,
                    n.Nome,
                    UsuariosEquipe = n.UsuariosEquipe != null ? n.UsuariosEquipe.Select(u => new
                    {
                        u.IdUsuario,
                        u.Nome,
                        u.Cargo,
                        u.Email
                    }).ToList() : null
                }).ToListAsync();
        }

        // GET: api/Equipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetEquipe(int id)
        {
          if (_context.Equipe == null)
          {
              return NotFound();
          }
            var equipe = await _context.Equipe.Include(n => n.UsuariosEquipe)
                .Select(n => new
                {
                    n.IdEquipe,
                    n.Nome,
                    UsuariosEquipe = n.UsuariosEquipe != null ? n.UsuariosEquipe.Select(u => new
                    {
                        u.IdUsuario,
                        u.Nome,
                        u.Cargo,
                        u.Email
                    }).ToList() : null
                }).FirstOrDefaultAsync(n => n.IdEquipe == id);

            if (equipe == null)
            {
                return NotFound();
            }

            return Ok(equipe);
        }

        // PUT: api/Equipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipe(int id, Equipe equipe)
        {
            if (id != equipe.IdEquipe)
            {
                return BadRequest();
            }

            _context.Entry(equipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipeExists(id))
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

        // POST: api/Equipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Equipe>> PostEquipe(Equipe equipe)
        {
          if (_context.Equipe == null)
          {
              return Problem("Entity set 'API_PontoContext.Equipe'  is null.");
          }
            _context.Equipe.Add(equipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEquipe", new { id = equipe.IdEquipe }, equipe);
        }

        // DELETE: api/Equipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipe(int id)
        {
            if (_context.Equipe == null)
            {
                return NotFound();
            }
            var equipe = await _context.Equipe.FindAsync(id);
            if (equipe == null)
            {
                return NotFound();
            }

            _context.Equipe.Remove(equipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquipeExists(int id)
        {
            return (_context.Equipe?.Any(e => e.IdEquipe == id)).GetValueOrDefault();
        }
    }
}
