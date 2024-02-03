using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiLaboratorio.Models
{
    public class Carrera
    {
        [Key]
        public int CarreraId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(255)]
        public string Descripcion { get; set; }

        public List<Materia> Materias { get; set; }
    }
}
