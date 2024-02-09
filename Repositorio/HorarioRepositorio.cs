using ApiLaboratorio.Data;
using ApiLaboratorio.Models;
using ApiLaboratorio.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLaboratorio.Repositorio
{
    public class HorarioRepositorio : IHorarioRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HorarioRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<HorarioDto>> GetHorarios()
        {
            var horarios = await _context.Horarios.ToListAsync();
            return _mapper.Map<List<HorarioDto>>(horarios);
        }

        public async Task<HorarioDto> GetHorarioById(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            return _mapper.Map<HorarioDto>(horario);
        }

        public async Task<HorarioDto> CreateUpdate(HorarioDto horarioDto)
        {
            Horario horario = _mapper.Map<HorarioDto, Horario>(horarioDto);
            if (horarioDto.HorarioID > 0)
            {
                _context.Horarios.Update(horario);
            }
            else
            {
                await _context.Horarios.AddAsync(horario);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<HorarioDto>(horario);
        }

        public async Task<bool> DeleteHorario(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null)
                return false;

            _context.Horarios.Remove(horario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
