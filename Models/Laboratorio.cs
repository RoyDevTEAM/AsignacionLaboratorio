using System.ComponentModel.DataAnnotations;

namespace ApiLaboratorio.Models
{
    public class Laboratorio
    {
        [Key]
        public int Laboratorio_id { get; set; }

        [Required]
        [MaxLength(15)]
        public string Nombre { get; set; }

        public int Capacidad { get; set; }

        [MaxLength(50)]
        public string Estado { get; set; }
    }
}
