namespace ApiLaboratorio.Models.Dto
{
    public class AsignarMateriaDto
    {
        public int id_Asignacion { get; set; }
        public MateriaDto Materia { get; set; }
        public CarreraDto Carrera { get; set; }
    }
}
