﻿using System;
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
    public class UsuariosController : ControllerBase
    {
        private readonly API_PontoContext _context;

        public UsuariosController(API_PontoContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetUsuario()
        {
          if (_context.Usuario == null)
          {
              return NotFound();
          }
            return await _context.Usuario.Include(n => n.HorarioUsuario).Include(n => n.EquipeUsuario).Include(n => n.FolgaUsuario)
                .Select(n => new
                {
                    n.IdUsuario,
                    HorarioUsuario = new
                    {
                        n.HorarioUsuario.IdHorario,
                        n.HorarioUsuario.HorarioEntrada,
                        n.HorarioUsuario.HorarioPausa,
                        n.HorarioUsuario.HorarioRetorno,
                        n.HorarioUsuario.HorarioSaida
                    },
                    EquipeUsuario = new
                    {
                        n.EquipeUsuario.IdEquipe,
                        n.EquipeUsuario.Nome
                    },
                    FolgaUsuario = new
                    {
                        n.FolgaUsuario.IdFolga,
                        n.FolgaUsuario.DiaSemana
                    },
                    n.Nome,
                    n.Foto,
                    n.Cargo,
                    n.Email,
                    n.QuantidadeFerias
                }).ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetUsuario(int id)
        {
          if (_context.Usuario == null)
          {
              return NotFound();
          }
            var usuario = await _context.Usuario.Include(n => n.HorarioUsuario).Include(n => n.EquipeUsuario).Include(n => n.FolgaUsuario)
                .Select(n => new
                {
                    n.IdUsuario,
                    HorarioUsuario = new
                    {
                        n.HorarioUsuario.IdHorario,
                        n.HorarioUsuario.HorarioEntrada,
                        n.HorarioUsuario.HorarioPausa,
                        n.HorarioUsuario.HorarioRetorno,
                        n.HorarioUsuario.HorarioSaida
                    },
                    EquipeUsuario = new
                    {
                        n.EquipeUsuario.IdEquipe,
                        n.EquipeUsuario.Nome
                    },
                    FolgaUsuario = new
                    {
                        n.FolgaUsuario.IdFolga,
                        n.FolgaUsuario.DiaSemana
                    },
                    n.Nome,
                    n.Foto,
                    n.Cargo,
                    n.Email,
                    n.QuantidadeFerias
                }).FirstOrDefaultAsync(n => n.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
          if (_context.Usuario == null)
          {
              return Problem("Entity set 'API_PontoContext.Usuario'  is null.");
          }
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.IdUsuario }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (_context.Usuario == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuario?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}
