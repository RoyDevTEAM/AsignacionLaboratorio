using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLaboratorio.Models
{
    public class Asignacion
    {
        [Key]
        public int Id { get; set; }

        public int IdUserRol { get; set; }

        [ForeignKey("IdUserRol")]
        public RolUsuario RolUsuario { get; set; }

        public int IdAsignacion { get; set; }

        [ForeignKey("IdAsignacion")]
        public AsignarMateria AsignarMateria { get; set; }

        public int LaboratorioId { get; set; }

        [ForeignKey("LaboratorioId")]
        public Laboratorio Laboratorio { get; set; }

        public int HorarioID { get; set; }

        [ForeignKey("HorarioID")]
        public Horario Horario { get; set; }
    }
}
