using System.ComponentModel.DataAnnotations;

namespace API_Ponto.Models
{
    public class Folga
    {
        [Key]
        public int IdFolga { get; set; }

        [Required(ErrorMessage = "Os dias da semana são obrigatórios", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 5)]
        public required string DiaSemana { get; set; }

        public List<Usuario>? UsuariosFolga { get; set; }
    }
}
