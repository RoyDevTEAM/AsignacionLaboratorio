using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLaboratorio.Models
{
    public class AsignarMateria
    {
        [Key]
        public int id_Asignacion { get; set; }

        public int Materia_id { get; set; }
        public int Carrera_id { get; set; }

        [ForeignKey("Materia_id")]
        public Materia Materia { get; set; }

        [ForeignKey("Carrera_id")]
        public Carrera Carrera { get; set; }
    }
}
