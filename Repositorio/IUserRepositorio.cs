using System.Threading.Tasks;
using ApiLaboratorio.Models;

namespace ApiLaboratorio.Repositorio
{
    public interface IUserRepositorio
    {
        Task<string> Register(User user, string password);
        Task<string> Login(string userName, string password);
        Task<bool> UserExiste(string username);
    }
}
