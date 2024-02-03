using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiLaboratorio.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // Relación con roles asi podremos ver el rol que tiene este usuario
        public List<Role> Roles { get; set; }
    }
}
