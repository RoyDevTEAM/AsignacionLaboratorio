using System.ComponentModel.DataAnnotations;

namespace ApiLaboratorio.Models
{
    public class Facultad
    {
        [Key]
        public int FacultadID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
