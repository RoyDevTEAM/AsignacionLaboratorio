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
    public class MateriaRepositorio : IMateriaRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MateriaRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MateriaDto> CreateUpdate(MateriaDto materiaDto)
        {
            try
            {
                Materia materia = _mapper.Map<MateriaDto, Materia>(materiaDto);
                if (materia.Materia_id > 0)
                {
                    _context.Materias.Update(materia);
                }
                else
                {
                    await _context.Materias.AddAsync(materia);
                }
                await _context.SaveChangesAsync();
                return _mapper.Map<MateriaDto>(materia);
            }
            catch (Exception ex)
            {
                // Manejar excepción
                throw ex;
            }
        }

        public async Task<bool> DeleteMateria(int id)
        {
            try
            {
                Materia materia = await _context.Materias.FindAsync(id);
                if (materia == null)
                {
                    return false;
                }
                _context.Materias.Remove(materia);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Manejar excepción
                throw ex;
            }
        }

        public async Task<MateriaDto> GetMateriaById(int id)
        {
            try
            {
                Materia materia = await _context.Materias.FindAsync(id);
                return _mapper.Map<MateriaDto>(materia);
            }
            catch (Exception ex)
            {
                // Manejar excepción
                throw ex;
            }
        }

        public async Task<List<MateriaDto>> GetMaterias()
        {
            try
            {
                List<Materia> materias = await _context.Materias.ToListAsync();
                return _mapper.Map<List<MateriaDto>>(materias);
            }
            catch (Exception ex)
            {
                // Manejar excepción
                throw ex;
            }
        }
    }
}
