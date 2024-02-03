using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiLaboratorio.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }

        // Relación con usuarios asi podremos ver los usuarios asociados a este rol
        public List<User> Users { get; set; }
    }
}
