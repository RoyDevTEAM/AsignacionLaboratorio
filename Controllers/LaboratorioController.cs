using Microsoft.AspNetCore.Mvc;
using ApiLaboratorio.Models.Dto;
using ApiLaboratorio.Repositorio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLaboratorio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratorioController : ControllerBase
    {
        private readonly ILaboratorioRepositorio _laboratorioRepositorio;
        protected ResponseDto _response;

        public LaboratorioController(ILaboratorioRepositorio laboratorioRepositorio)
        {
            _laboratorioRepositorio = laboratorioRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/Laboratorio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LaboratorioDto>>> GetLaboratorios()
        {
            try
            {
                var laboratorios = await _laboratorioRepositorio.GetLaboratorios();
                _response.Result = laboratorios;
                _response.DisplayMessage = "Lista de Laboratorios";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        // GET: api/Laboratorio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LaboratorioDto>> GetLaboratorio(int id)
        {
            var laboratorio = await _laboratorioRepositorio.GetLaboratorioById(id);
            if (laboratorio == null)
            {
                _response.IsSuccess= false;
                _response.DisplayMessage = "Laboratorio no encontrado";
                return NotFound(_response);
            }
            _response.Result = laboratorio;
            _response.DisplayMessage = "Información del Laboratorio";
            return Ok(_response);
        }

        // POST: api/Laboratorio
        [HttpPost]
        public async Task<ActionResult<LaboratorioDto>> PostLaboratorio(LaboratorioDto laboratorioDto)
        {
            try
            {
                var laboratorio = await _laboratorioRepositorio.CreateUpdateLaboratorio(laboratorioDto);
                _response.Result = laboratorio;
                _response.DisplayMessage = "Laboratorio creado correctamente";
                return CreatedAtAction(nameof(GetLaboratorio), new { id = laboratorio.Laboratorio_id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al crear el laboratorio";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // PUT: api/Laboratorio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLaboratorio(int id, LaboratorioDto laboratorioDto)
        {
            try
            {
                if (id != laboratorioDto.Laboratorio_id)
                {
                    return BadRequest();
                }

                var laboratorio = await _laboratorioRepositorio.CreateUpdateLaboratorio(laboratorioDto);
                _response.Result = laboratorio;
                _response.DisplayMessage = "Laboratorio actualizado correctamente";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar el laboratorio";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // DELETE: api/Laboratorio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLaboratorio(int id)
        {
            try
            {
                var result = await _laboratorioRepositorio.DeleteLaboratorio(id);
                if (result)
                {
                    _response.DisplayMessage = "Laboratorio eliminado correctamente";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Laboratorio no encontrado";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al eliminar el laboratorio";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }
    }
}
