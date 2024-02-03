using ApiLaboratorio.Models;
using ApiLaboratorio.Models.Dto;
using ApiLaboratorio.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLaboratorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersControllers : ControllerBase
    {
        private readonly IUserRepositorio _userRepositorio;
        protected ResponseDto _response;
        public UsersControllers(IUserRepositorio userRepositorio)
        {
            _userRepositorio = userRepositorio;
            _response = new ResponseDto();
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserDto user)
        {
            var respuesta = await _userRepositorio.Register(
                new User
                {
                    UserName = user.UserName
                }, user.Password);
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
            _response.DisplayMessage = "Usuario Creado con Exito";
            //_response.Result = respuesta;
            JwTPackage jpt = new JwTPackage();
            jpt.UserName = user.UserName;
            jpt.Token = respuesta;
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
                _response.DisplayMessage = "Password Incorrecta";
                return BadRequest(_response);
            }

            //_response.Result = respuesta;
            JwTPackage jpt = new JwTPackage();
            jpt.UserName = user.UserName;
            jpt.Token = respuesta;
            _response.Result = jpt;


            _response.DisplayMessage = "Usuario Conectado";
            return Ok(_response);
        }
    }
    public class JwTPackage
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}


