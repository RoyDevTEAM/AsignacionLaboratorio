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
    public class LaboratorioRepositorio : ILaboratorioRepositorio
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LaboratorioRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LaboratorioDto>> GetLaboratorios()
        {
            var laboratorios = await _context.Laboratorios.ToListAsync();
            return _mapper.Map<List<LaboratorioDto>>(laboratorios);
        }

        public async Task<LaboratorioDto> GetLaboratorioById(int id)
        {
            var laboratorio = await _context.Laboratorios.FindAsync(id);
            return _mapper.Map<LaboratorioDto>(laboratorio);
        }

        public async Task<LaboratorioDto> CreateUpdateLaboratorio(LaboratorioDto laboratorioDto)
        {
            Laboratorio laboratorio = _mapper.Map<LaboratorioDto, Laboratorio>(laboratorioDto);
            if (laboratorioDto.Laboratorio_id > 0)
            {
                _context.Laboratorios.Update(laboratorio);
            }
            else
            {
                await _context.Laboratorios.AddAsync(laboratorio);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<LaboratorioDto>(laboratorio);
        }

        public async Task<bool> DeleteLaboratorio(int id)
        {
            var laboratorio = await _context.Laboratorios.FindAsync(id);
            if (laboratorio == null)
                return false;

            _context.Laboratorios.Remove(laboratorio);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
