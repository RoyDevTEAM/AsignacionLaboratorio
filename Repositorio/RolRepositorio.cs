using ApiLaboratorio.Data;
using ApiLaboratorio.Models;
using ApiLaboratorio.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ApiLaboratorio.Repositorio
{
    public class RolRepositorio : IRolRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RolRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoleDto> GetRolById(int rolId)
        {
            var rol = await _context.Roles.FindAsync(rolId);
            return _mapper.Map<RoleDto>(rol);
        }

        public async Task<RoleDto> GetRolByName(string rolName)
        {
            var rol = await _context.Roles.FirstOrDefaultAsync(r => r.Nombre == rolName);
            return _mapper.Map<RoleDto>(rol);
        }
    }
}
