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
    public class FacultadController : ControllerBase
    {
        private readonly IFacultadRepositorio _facultadRepositorio;
        protected ResponseDto _response;

        public FacultadController(IFacultadRepositorio facultadRepositorio)
        {
            _facultadRepositorio = facultadRepositorio;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacultadDto>>> GetFacultades()
        {
            try
            {
                var lista = await _facultadRepositorio.GetFacultades();
                _response.Result = lista;
                _response.DisplayMessage = "Lista de Facultades";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FacultadDto>> GetFacultad(int id)
        {
            var facultad = await _facultadRepositorio.GetFacultadById(id);
            if (facultad == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Facultad no encontrada";
                return NotFound(_response);
            }
            _response.Result = facultad;
            _response.DisplayMessage = "Información de la Facultad";
            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<FacultadDto>> PostFacultad(FacultadDto facultadDto)
        {
            try
            {
                var model = await _facultadRepositorio.CreateUpdateFacultad(facultadDto);
                _response.Result = model;
                return CreatedAtAction(nameof(GetFacultad), new { id = model.FacultadID }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al crear o actualizar la Facultad";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacultad(int id)
        {
            try
            {
                bool isDeleted = await _facultadRepositorio.DeleteFacultad(id);
                if (isDeleted)
                {
                    _response.Result = isDeleted;
                    _response.DisplayMessage = "Facultad eliminada";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error al eliminar la Facultad";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }
    }
}
