using System.Threading.Tasks;
using ApiLaboratorio.Models;
using ApiLaboratorio.Models.Dto;

namespace ApiLaboratorio.Repositorio
{
    public interface IUserRepositorio
    {
        Task<string> Register(UserDto userDto);
        Task<string> Login(string userName, string password);
        Task<bool> UserExists(string username);
        Task<UserDto> GetUserByName(string username);
        Task<bool> AsignarRol(int userId, int rolId);

    }
}
