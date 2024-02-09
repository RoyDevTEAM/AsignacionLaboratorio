namespace ApiLaboratorio.Models.Dto
{
    public class RolUsuarioDto
    {
        public int IdUserRol { get; set; }

        public int UsuarioId { get; set; }

        public UserDto Usuario { get; set; }

        public int RolId { get; set; }

        public RoleDto Rol { get; set; }
    }
}
