using System.ComponentModel.DataAnnotations;

namespace API_Ponto.Models
{
    public class Equipe
    {
        [Key]
        public int IdEquipe { get; set; }

        [Required(ErrorMessage = "O nome da equipe é obrigatório", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 2)]
        public required string Nome { get; set; }

        public List<Usuario>? UsuariosEquipe { get; set; }
    }
}
