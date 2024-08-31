using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API_Ponto.Models;

namespace API_Ponto.Data
{
    public class API_PontoContext : DbContext
    {
        public API_PontoContext (DbContextOptions<API_PontoContext> options)
            : base(options)
        {
        }

        public DbSet<API_Ponto.Models.Equipe> Equipe { get; set; } = default!;

        public DbSet<API_Ponto.Models.Folga> Folga { get; set; } = default!;

        public DbSet<API_Ponto.Models.HorarioTrabalho> HorarioTrabalho { get; set; } = default!;

        public DbSet<API_Ponto.Models.Usuario> Usuario { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.EquipeUsuario)
                .WithMany(e => e.UsuariosEquipe)
                .HasForeignKey(u => u.IdEquipe);

            modelBuilder.Entity<Equipe>()
                .HasMany(e => e.UsuariosEquipe)
                .WithOne(u => u.EquipeUsuario)
                .HasForeignKey(u => u.IdEquipe);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.HorarioUsuario)
                .WithMany(h => h.UsuariosHorario)
                .HasForeignKey(u => u.IdHorario);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.FolgaUsuario)
                .WithMany(f => f.UsuariosFolga)
                .HasForeignKey(u => u.IdFolga);


        }
    }
}
