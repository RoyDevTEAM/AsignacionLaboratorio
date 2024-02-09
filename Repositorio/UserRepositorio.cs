using ApiLaboratorio.Data;
using ApiLaboratorio.Models;
using ApiLaboratorio.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiLaboratorio.Repositorio
{
    public class UserRepositorio : IUserRepositorio
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public UserRepositorio(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<string> Login(string userName, string password)
        {
            var user = await _db.Usuarios.FirstOrDefaultAsync(
                x => x.UserName.ToLower().Equals(userName.ToLower()));

            if (user == null)
            {
                return "nouser";
            }
            else if (!VerificarPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return "wrongpassword";
            }
            else
            {
                return CrearToken(user);
            }
        }

        public async Task<string> Register(UserDto userDto)
        {
            try
            {
                if (await UserExists(userDto.UserName))
                {
                    return "existe";
                }

                var user = new User
                {
                    UserName = userDto.UserName,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email
                };

                CrearPasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _db.Usuarios.AddAsync(user);
                await _db.SaveChangesAsync();

                return CrearToken(user);
            }
            catch (Exception)
            {
                return "error";
            }
        }

        public async Task<bool> UserExists(string username)
        {
            return await _db.Usuarios.AnyAsync(x => x.UserName.ToLower().Equals(username.ToLower()));
        }

        public async Task<UserDto> GetUserByName(string username)
        {
            var user = await _db.Usuarios.FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(username.ToLower()));
            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerificarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CrearToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public async Task<bool> AsignarRol(int userId, int rolId)
        {
            var user = await _db.Usuarios.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.Id == userId);
            var rol = await _db.Roles.FirstOrDefaultAsync(r => r.RolId == rolId);

            if (user == null || rol == null)
            {
                return false; // Usuario o rol no encontrados
            }

            // Verificar si el usuario ya tiene asignado este rol
            if (user.UserRoles != null && user.UserRoles.Any(ur => ur.RolId == rolId))
            {
                return false; // El usuario ya tiene asignado este rol
            }

            // Crear una nueva entrada de UserRole para asignar el rol al usuario
            var userRole = new RolUsuario
            {
                UsuarioId = userId,
                RolId = rolId
            };

            // Si el usuario no tiene roles, inicializar la lista antes de agregar el nuevo rol
            if (user.UserRoles == null)
            {
                user.UserRoles = new List<RolUsuario>();
            }

            // Agregar el UserRole al conjunto de roles del usuario
            user.UserRoles.Add(userRole);

            _db.Usuarios.Update(user);
            await _db.SaveChangesAsync();

            return true;
        }


    }
}
