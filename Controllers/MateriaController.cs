using ApiLaboratorio.Models.Dto;
using ApiLaboratorio.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLaboratorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly IMateriaRepositorio _materiaRepositorio;
        private readonly ResponseDto _response;

        public MateriaController(IMateriaRepositorio materiaRepositorio)
        {
            _materiaRepositorio = materiaRepositorio;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MateriaDto>>> GetMaterias()
        {
            try
            {
                var materias = await _materiaRepositorio.GetMaterias();
                _response.Result = materias;
                _response.DisplayMessage = "Lista de Materias";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MateriaDto>> GetMateria(int id)
        {
            try
            {
                var materia = await _materiaRepositorio.GetMateriaById(id);
                if (materia == null)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Materia no encontrada";
                    return NotFound(_response);
                }
                _response.Result = materia;
                _response.DisplayMessage = "Información de la Materia";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<MateriaDto>> PostMateria(MateriaDto materiaDto)
        {
            try
            {
                var materia = await _materiaRepositorio.CreateUpdate(materiaDto);
                _response.Result = materia;
                _response.DisplayMessage = "Materia creada correctamente";
                return CreatedAtAction(nameof(GetMateria), new { id = materia.Materia_id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateria(int id, MateriaDto materiaDto)
        {
            try
            {
                var materia = await _materiaRepositorio.CreateUpdate(materiaDto);
                _response.Result = materia;
                _response.DisplayMessage = "Materia actualizada correctamente";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateria(int id)
        {
            try
            {
                var isDeleted = await _materiaRepositorio.DeleteMateria(id);
                if (!isDeleted)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Materia no encontrada";
                    return NotFound(_response);
                }
                _response.Result = isDeleted;
                _response.DisplayMessage = "Materia eliminada correctamente";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}
