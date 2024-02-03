namespace ApiLaboratorio.Models.Dto
{
    public class MateriaDto
    {
        public int MateriaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CarreraId { get; set; }  // Para representar la relación con Carrera
    }
}
