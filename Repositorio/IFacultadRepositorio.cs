using ApiLaboratorio.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLaboratorio.Repositorio
{
    public interface IFacultadRepositorio
    {
        Task<List<FacultadDto>> GetFacultades();
        Task<FacultadDto> GetFacultadById(int id);
        Task<FacultadDto> CreateUpdateFacultad(FacultadDto facultadDto);
        Task<bool> DeleteFacultad(int id);
    }
}
