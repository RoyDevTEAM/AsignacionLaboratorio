namespace ApiLaboratorio.Models.Dto
{
    public class AsignacionDto
    {
        public int Id { get; set; }

        public int IdUserRol { get; set; }

        public RolUsuarioDto RolUsuario { get; set; }

        public int IdAsignacion { get; set; }

        public AsignarMateriaDto AsignarMateria { get; set; }

        public int LaboratorioId { get; set; }

        public LaboratorioDto Laboratorio { get; set; }

        public int HorarioID { get; set; }

        public HorarioDto Horario { get; set; }
    }
}
