using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiLaboratorio.Models
{
    public class Materia
    {

        [Key]
        public int Materia_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        // Relación muchos a muchos con Carrera a través de Asignar_Materia
        public ICollection<AsignarMateria> AsignarMaterias { get; set; }
    }
}
