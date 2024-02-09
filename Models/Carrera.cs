using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiLaboratorio.Models
{
    public class Carrera
    {
        [Key]
        public int CarreraId { get; set; }

        [Required]
        [MaxLength(70)]
        public string Nombre { get; set; }

        // Relación uno a muchos con AsignarMateria
        public ICollection<AsignarMateria> AsignarMaterias { get; set; }
    }
}
