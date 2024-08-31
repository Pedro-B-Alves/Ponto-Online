using System.ComponentModel.DataAnnotations;

namespace API_Ponto.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        public int IdHorario { get; set; }
        public required HorarioTrabalho HorarioUsuario { get; set; }

        public int IdEquipe { get; set; }
        public required Equipe EquipeUsuario { get; set; }

        public int IdFolga { get; set; }
        public required Folga FolgaUsuario { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 2)]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "A foto do usuário é obrigatória", AllowEmptyStrings = false)]
        public required string Foto { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string? Cargo { get; set; }

        [Required(ErrorMessage = "Informe o seu email", AllowEmptyStrings = false)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, MinimumLength = 6)]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Uma senha é obrigatória", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(200, MinimumLength = 5)]
        public required string Senha { get; set; }

        public int QuantidadeFerias { get; set; }
    }
}
