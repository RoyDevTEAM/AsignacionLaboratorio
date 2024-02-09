using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLaboratorio.Models
{
    public class RolUsuario
    {
        [Key]
        public int IdUserRol { get; set; }

        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public User Usuario { get; set; }

        public int RolId { get; set; }

        [ForeignKey("RolId")]
        public Role Rol { get; set; }
    }
}
