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
    public class HorarioController : ControllerBase
    {
        private readonly IHorarioRepositorio _horarioRepositorio;
        private ResponseDto _response;

        public HorarioController(IHorarioRepositorio horarioRepositorio)
        {
            _horarioRepositorio = horarioRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/Horario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioDto>>> GetHorarios()
        {
            try
            {
                var horarios = await _horarioRepositorio.GetHorarios();
                _response.Result = horarios;
                _response.DisplayMessage = "Lista de Horarios obtenida con éxito";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = $"Error al obtener los horarios: {ex.Message}";
                return BadRequest(_response);
            }
        }

        // GET: api/Horario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HorarioDto>> GetHorario(int id)
        {
            try
            {
                var horario = await _horarioRepositorio.GetHorarioById(id);
                if (horario == null)
                {
                    _response.IsSuccess   = false;
                    _response.DisplayMessage = "Horario no encontrado";
                    return NotFound(_response);
                }
                _response.Result = horario;
                _response.DisplayMessage = "Horario encontrado";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = $"Error al obtener el horario: {ex.Message}";
                return BadRequest(_response);
            }
        }

        // POST: api/Horario
        [HttpPost]
        public async Task<ActionResult<HorarioDto>> PostHorario(HorarioDto horarioDto)
        {
            try
            {
                var createdHorario = await _horarioRepositorio.CreateUpdate(horarioDto);
                _response.Result = createdHorario;
                _response.DisplayMessage = "Horario creado exitosamente";
                return CreatedAtAction(nameof(GetHorario), new { id = createdHorario.HorarioID }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = $"Error al crear el horario: {ex.Message}";
                return BadRequest(_response);
            }
        }

        // PUT: api/Horario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorario(int id, HorarioDto horarioDto)
        {
            if (id != horarioDto.HorarioID)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "ID del horario no coincide con el objeto proporcionado";
                return BadRequest(_response);
            }

            try
            {
                await _horarioRepositorio.CreateUpdate(horarioDto);
                _response.DisplayMessage = "Horario actualizado exitosamente";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = $"Error al actualizar el horario: {ex.Message}";
                return BadRequest(_response);
            }
        }

        // DELETE: api/Horario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            try
            {
                var isDeleted = await _horarioRepositorio.DeleteHorario(id);
                if (isDeleted)
                {
                    _response.Result = isDeleted;
                    _response.DisplayMessage = "Horario eliminado exitosamente";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error al eliminar el horario";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = $"Error al eliminar el horario: {ex.Message}";
                return BadRequest(_response);
            }
        }
    }
}
