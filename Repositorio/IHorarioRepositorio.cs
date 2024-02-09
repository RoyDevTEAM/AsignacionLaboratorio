using ApiLaboratorio.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLaboratorio.Repositorio
{
    public interface IHorarioRepositorio
    {
        Task<List<HorarioDto>> GetHorarios();
        Task<HorarioDto> GetHorarioById(int id);
        Task<HorarioDto> CreateUpdate(HorarioDto horarioDto);
        Task<bool> DeleteHorario(int id);
    }
}
