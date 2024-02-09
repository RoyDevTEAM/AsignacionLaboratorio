
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ApiLaboratorio.Models.Dto;
using ApiLaboratorio.Models;
using ApiLaboratorio.Repositorio;
using ApiArticulos.Models.Dto;

namespace ApiArticulos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepositorio _userRepositorio;
        private readonly IRolRepositorio _rolRepositorio;
        private readonly ResponseDto _response;

        public UsersController(IUserRepositorio userRepositorio, IRolRepositorio rolRepositorio)
        {
            _userRepositorio = userRepositorio;
            _rolRepositorio = rolRepositorio;
            _response = new ResponseDto();
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserDto user)
        {
            var respuesta = await _userRepositorio.Register(user);

            if (respuesta == "existe")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usuario ya Existe";
                return BadRequest(_response);
            }

            if (respuesta == "error")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al Crear el Usuario";
                return BadRequest(_response);
            }

            _response.DisplayMessage = "Usuario Creado con Éxito";
            JwTPackage jpt = new JwTPackage
            {
                UserName = user.UserName,
                Token = respuesta
            };
            _response.Result = jpt;

            return Ok(_response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserDto user)
        {
            var respuesta = await _userRepositorio.Login(user.UserName, user.Password);

            if (respuesta == "nouser")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usuario no Existe";
                return BadRequest(_response);
            }

            if (respuesta == "wrongpassword")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Contraseña Incorrecta";
                return BadRequest(_response);
            }

            JwTPackage jpt = new JwTPackage
            {
                UserName = user.UserName,
                Token = respuesta
            };
            _response.Result = jpt;

            _response.DisplayMessage = "Usuario Conectado";
            return Ok(_response);
        }

        [HttpPost("AssignRole")]
        public async Task<ActionResult> AssignRole(AssignRoleDto assignRole)
        {
            var userExists = await _userRepositorio.UserExists(assignRole.UserName);
            if (!userExists)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usuario no Existe";
                return BadRequest(_response);
            }

            var rol = await _rolRepositorio.GetRolById(assignRole.RolId);
            if (rol == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Rol no Existe";
                return BadRequest(_response);
            }

            var asignacionExitosa = await _userRepositorio.AsignarRol(assignRole.UserId, assignRole.RolId);
            if (!asignacionExitosa)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al Asignar el Rol al Usuario";
                return BadRequest(_response);
            }

            // Verificar si la asignación fue exitosa o si el usuario ya tenía el rol asignado
            var mensaje = asignacionExitosa ? "Rol Asignado con Éxito" : "El Usuario ya tiene este Rol asignado";
            _response.DisplayMessage = mensaje;
            return Ok(_response);
        }

    }

    public class JwTPackage
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
