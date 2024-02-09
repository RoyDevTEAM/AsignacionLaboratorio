using System.ComponentModel.DataAnnotations;

namespace ApiLaboratorio.Models
{
    public class Role
    {
        [Key]
        public int RolId { get; set; }

        public string Nombre { get; set; }
    }
}
