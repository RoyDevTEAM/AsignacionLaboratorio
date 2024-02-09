using System.ComponentModel.DataAnnotations;

namespace ApiLaboratorio.Models
{
    public class Horario
    {
        [Key]
        public int HorarioID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Hora { get; set; }

        [Required]
        [MaxLength(20)]
        public string Turno { get; set; }

        [Required]
        [MaxLength(50)]
        public string Dia { get; set; }
    }
}
