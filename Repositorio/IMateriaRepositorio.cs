using ApiLaboratorio.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLaboratorio.Repositorio
{
    public interface IMateriaRepositorio
    {
        Task<List<MateriaDto>> GetMaterias();
        Task<MateriaDto> GetMateriaById(int id);
        Task<MateriaDto> CreateUpdate(MateriaDto materiaDto);
        Task<bool> DeleteMateria(int id);
    }
}
