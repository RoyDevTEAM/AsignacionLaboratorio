using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiLaboratorio.Models.Dto;
using ApiLaboratorio.Repositorio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Org.BouncyCastle.Utilities.Collections;

namespace ApiLaboratorio.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class CarreraController : ControllerBase
    {
        private readonly ICarreraRepositorio _carreraRepositorio;
        private readonly ResponseDto _response;

        public CarreraController(ICarreraRepositorio carreraRepositorio)
        {
            _carreraRepositorio = carreraRepositorio;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarreraDto>>> GetCarreras()
        {
            try
            {
                var carreras = await _carreraRepositorio.GetCarreras();
                _response.Result = carreras;
                _response.DisplayMessage = "Lista de Carreras";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarreraDto>> GetCarrera(int id)
        {
            try
            {
                var carrera = await _carreraRepositorio.GetCarreraById(id);
                if (carrera == null)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Carrera no encontrada";
                    return NotFound(_response);
                }
                _response.Result = carrera;
                _response.DisplayMessage = "Información de la Carrera";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CarreraDto>> PostCarrera(CarreraDto carreraDto)
        {
            try
            {
                var createdCarrera = await _carreraRepositorio.CreateUpdateCarrera(carreraDto);
                _response.Result = createdCarrera;
                return CreatedAtAction(nameof(GetCarrera), new { id = createdCarrera.CarreraId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CarreraDto>> PutCarrera(int id, CarreraDto carreraDto)
        {
            try
            {
                carreraDto.CarreraId = id;
                var updatedCarrera = await _carreraRepositorio.CreateUpdateCarrera(carreraDto);
                if (updatedCarrera == null)
                {
                    _response.IsSuccess  = false;
                    _response.DisplayMessage = "Carrera no encontrada";
                    return NotFound(_response);
                }
                _response.Result = updatedCarrera;
                _response.DisplayMessage = "Carrera actualizada";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCarrera(int id)
        {
            try
            {
                var deleted = await _carreraRepositorio.DeleteCarrera(id);
                if (!deleted)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Carrera no encontrada";
                    return NotFound(_response);
                }
                _response.Result = true;
                _response.DisplayMessage = "Carrera eliminada";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}

/*--Roles
INSERT INTO Roles (Nombre)
VALUES
  ('Decano'),
  ('Docente'),
  ('Auxiliar');*/