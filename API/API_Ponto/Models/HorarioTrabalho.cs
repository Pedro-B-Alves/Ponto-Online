using System.ComponentModel.DataAnnotations;

namespace API_Ponto.Models
{
    public class HorarioTrabalho
    {
        [Key]
        public int IdHorario { get; set; }

        [Required(ErrorMessage = "Um horário de entrada é obrigatório", AllowEmptyStrings = false)]
        [DataType(DataType.Time)]
        public TimeSpan HorarioEntrada { get; set; }

        [Required(ErrorMessage = "Um horário de pausa é obrigatório", AllowEmptyStrings = false)]
        [DataType(DataType.Time)]
        public TimeSpan HorarioPausa { get; set; }

        [Required(ErrorMessage = "Um horário de retorno é obrigatório", AllowEmptyStrings = false)]
        [DataType(DataType.Time)]
        public TimeSpan HorarioRetorno { get; set; }

        [Required(ErrorMessage = "Um horário de saída é obrigatório", AllowEmptyStrings = false)]
        [DataType(DataType.Time)]
        public TimeSpan HorarioSaida { get; set; }

        public List<Usuario>? UsuariosHorario { get; set; }
    }
}
