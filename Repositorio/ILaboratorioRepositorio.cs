using ApiLaboratorio.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLaboratorio.Repositorio
{
    public interface ILaboratorioRepositorio
    {
        Task<List<LaboratorioDto>> GetLaboratorios();
        Task<LaboratorioDto> GetLaboratorioById(int id);
        Task<LaboratorioDto> CreateUpdateLaboratorio(LaboratorioDto laboratorioDto);
        Task<bool> DeleteLaboratorio(int id);
    }
}
