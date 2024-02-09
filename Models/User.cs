using System.ComponentModel.DataAnnotations;

namespace ApiLaboratorio.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string UserName { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [MaxLength(255)]
        public string FirstName { get; set; }

        [MaxLength(255)]
        public string LastName { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }
        // Relación muchos a muchos con Role a través de UserRole
        public ICollection<RolUsuario> UserRoles { get; set; }
    }
}
