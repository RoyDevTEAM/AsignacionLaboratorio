using System.Threading.Tasks;
using ApiLaboratorio.Models;
using ApiLaboratorio.Models.Dto;

namespace ApiLaboratorio.Repositorio
{
    public interface IRolRepositorio
    {
        Task<RoleDto> GetRolById(int rolId);
        Task<RoleDto> GetRolByName(string rolName);
    }
}
