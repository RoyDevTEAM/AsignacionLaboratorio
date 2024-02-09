using ApiLaboratorio.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLaboratorio.Repositorio
{
    public interface ICarreraRepositorio
    {
        Task<List<CarreraDto>> GetCarreras();
        Task<CarreraDto> GetCarreraById(int id);
        Task<CarreraDto> CreateUpdateCarrera(CarreraDto carreraDto);
        Task<bool> DeleteCarrera(int id);
    }
}
